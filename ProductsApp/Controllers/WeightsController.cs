using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProductsApp.Models;

namespace ProductsApp.Controllers
{
    public class WeightsController : ApiController
    {
        //todo need to move to persistent data to work on chaining test data generation
        List<Weight> _weights = new List<Weight>
        {
            new Weight { Units = "Metric", Value = 100.4},
            new Weight { Units = "Metric", Value = 23.4},
            new Weight { Units = "Imperial", Value = 98.624},
        };


        public IEnumerable<Weight> GetAllWeights()
        {
            return _weights;
        }

        public IHttpActionResult GetWeight(double value)
        {
            var weight = _weights.FirstOrDefault(x => x.Value == value);
            if (weight == null)
            {
                return NotFound();
            }
            return Ok(weight);
        }

        public IHttpActionResult AddWeight(Weight weight)
        {
            _weights.Add(weight);
            return Ok(_weights);
        }
    }
}
