using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MOTILab4.Data;
using MOTILab4.Models;
using MOTILab4.VIewModels;

namespace MOTILab4.Controllers
{
    public class AlternativeVoicesController : Controller
    {
        private readonly MOTILab4Context _context;

        public AlternativeVoicesController(MOTILab4Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Elector> electors = await _context.Elector.ToListAsync();
            List<VotingObject> votingObjects = await _context.VotingObject.ToListAsync();
            List<ElectorChoise> electorChoises = await _context.ElectorChoise.ToListAsync();
            List<VotingObject> resultVotingObjects = await _context.VotingObject.ToListAsync();
            List<ElectorChoise> resultElectorChoises = await _context.ElectorChoise.ToListAsync();
            foreach (var choise in electorChoises)
            {
                choise.Elector = await _context.Elector.FirstAsync(i => i.ElectorId == choise.ElectorId);
                choise.VotingObject = await _context.VotingObject.FirstAsync(i => i.VotingObjectId == choise.VotingObjectId);
            }
            List<int> eliminationList = new List<int>();
            for(int j = 0; j<votingObjects.Count()-1; j++)
            {
                Dictionary<VotingObject, int> minVote = new Dictionary<VotingObject, int>();
                Dictionary<Elector, VotingObject> minChoise = new Dictionary<Elector, VotingObject>();
                foreach (var elector in electors)
                {
                    List<ElectorChoise> tempList = new List<ElectorChoise>();
                    foreach(var vote in resultElectorChoises)
                    {
                        if(vote.ElectorId==elector.ElectorId)
                        {
                            tempList.Add(vote);
                        }
                    }
                    int maxOrder = 0;
                    int maxOrderId = 0;
                    int counter = 0;
                    foreach(var item in tempList)
                    {
                        if(counter==0)
                        {
                            maxOrder = item.Order;
                            maxOrderId = item.VotingObjectId;
                        }
                        else
                        {
                            if(maxOrder<item.Order)
                            {
                                maxOrder = item.Order;
                                maxOrderId = item.VotingObjectId;
                            }
                        }
                    }
                    minChoise.Add(elector, resultVotingObjects.Where(i => i.VotingObjectId == maxOrderId).FirstOrDefault());
                }
                foreach(var choise in minChoise)
                {
                    if(minVote.ContainsKey(choise.Value))
                    {
                        minVote[choise.Value] += choise.Key.ElectorRatio;
                    }
                    else
                    {
                        minVote.Add(choise.Value, choise.Key.ElectorRatio);
                    }
                }
                /*foreach (var vote in resultVotingObjects)
                {
                    int votes = 0;
                    List<ElectorChoise> tempList = new List<ElectorChoise>();
                    foreach(var elector in electors)
                    {
                        ElectorChoise choise = resultElectorChoises.Where(i => i.VotingObjectId == vote.VotingObjectId && i.ElectorId == elector.ElectorId).Last();
                        tempList.Add(choise);
                    }
                    foreach (var choise in tempList)
                    {
                        Elector elector = await _context.Elector.FirstAsync(i => i.ElectorId == choise.ElectorId);
                        votes += elector.ElectorRatio;
                    }
                    minVote.Add(vote, votes);
                }*/
                int max = 0;
                VotingObject votingObjectMin = new VotingObject();
                int count = 0;
                foreach (var obj in resultVotingObjects)
                {
                    if(minVote.ContainsKey(obj))
                    {
                        if (count == 0)
                        {
                            max = minVote[obj];
                            votingObjectMin = obj;
                            count++;
                        }
                        else
                        {
                            if (max < minVote[obj])
                            {
                                max = minVote[obj];
                                votingObjectMin = obj;
                                count++;
                            }
                        }
                    }
                }
                eliminationList.Add(votingObjectMin.VotingObjectId);
                List<VotingObject> deleteObjects = await _context.VotingObject.Where(i => i.VotingObjectId == votingObjectMin.VotingObjectId).ToListAsync();
                List<ElectorChoise> deleteVoices = await _context.ElectorChoise.Where(i => i.VotingObjectId == votingObjectMin.VotingObjectId).ToListAsync();
                foreach (var del in deleteObjects)
                {
                    resultVotingObjects.Remove(del);
                }
                foreach(var del in deleteVoices)
                {
                    resultElectorChoises.Remove(del);
                }
            }
            AlternativeVoicesViewModel model = new AlternativeVoicesViewModel();
            model.Electors = electors;
            model.VotingObjects = votingObjects;
            model.ElectorChoises = electorChoises;
            model.EliminationList = eliminationList;
            return View(model);
        }
    }
}
