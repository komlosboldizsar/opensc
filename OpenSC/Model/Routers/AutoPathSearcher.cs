using OpenSC.Model.Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{
    
    public class AutoPathSearcher
    {

        public bool? Possible { get; private set; } = null;

        private List<Crosspoint> crosspoints = new List<Crosspoint>();

        public IReadOnlyList<Crosspoint> Crosspoints
            => crosspoints.AsReadOnly();

        public ExternalSignal Source { get; private set; }

        public RouterOutput Destination { get; private set; }
        public AutoPathSearcher(ExternalSignal source, RouterOutput destination)
        {

            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));

            Source = source;
            Destination = destination;

            search();

        }

        private void search()
        {

            List<RouterProxy> nodes = new List<RouterProxy>();
            foreach (Router router in RouterDatabase.Instance) {
                RouterProxy rp = new RouterProxy(router);
                nodes.Add(rp);
                // For all neighbors u to s, set d(u) = w(s,u)
                foreach (RouterInput input in router.Inputs)
                {
                    if (input.Source == Source)
                    {
                        rp.Input = input;
                        rp.TotalCost = 0;
                    }
                }
            }

            List<RouterProxy> visited = new List<RouterProxy>();

            while (visited.Count != nodes.Count)
            {

                // Choose {v {not in} S} with d(v) minimal
                RouterProxy selected = null;
                foreach (RouterProxy rp in nodes.Except(visited))
                {
                    bool smaller = false;
                    if (selected == null)
                        smaller = true;
                    else if ((rp.TotalCost != null) && (selected.TotalCost == null))
                        smaller = true;
                    else if ((rp.TotalCost != null) && (int)rp.TotalCost < (int)selected.TotalCost)
                        smaller = true;
                    if (smaller)
                        selected = rp;
                }
                
                // Set S = S {union} {v}
                visited.Add(selected);
                
                // For all neighbors q to v such that q {not in} S
                foreach (RouterProxy rp in nodes.Except(visited))
                {
                    foreach (RouterInput input in rp.Router.Inputs)
                    {

                        if (!input.IsTieline)
                            continue;

                        // Is neighbor?
                        if (!selected.Router.Outputs.Contains(input.Source))
                            continue;

                        if (selected.TotalCost == null)
                            continue;

                        int sumCost = (int)selected.TotalCost + (int)input.TielineCost;

                        if ((rp.TotalCost == null) || ((int)rp.TotalCost > sumCost))
                        {
                            rp.TotalCost = sumCost;
                            rp.Input = input;
                        }
                    }
                }

            }

            // Router of destination
            crosspoints.Clear();
            RouterProxy destRp = nodes.FirstOrDefault(rp => (rp.Router == Destination.Router));
            if (destRp.TotalCost == null)
            {
                Possible = false;
                return;
            }

            Possible = true;
            RouterProxy currentRp = destRp;
            RouterOutput dst = Destination;
            while (true)
            {
                crosspoints.Add(new Crosspoint(dst, currentRp.Input));
                if (currentRp.Input.Source == Source)
                    break;
                dst = (RouterOutput)currentRp.Input.Source;
                currentRp = nodes.FirstOrDefault(rp => (rp.Router == dst.Router));
            }

        }

        public class RouterProxy
        {

            public Router Router { get; private set; }
            public RouterInput Input { get; set; } = null;
            public int? TotalCost { get; set; } = null;

            public RouterProxy(Router router)
            {
                Router = router;
            }

        }

        public void TakeCrosspoints()
        {
            if (Possible == null)
                throw new Exception();
            if (Possible == false)
                throw new Exception();
            foreach (Crosspoint cp in Crosspoints)
                cp.Take();
        }

        public class Crosspoint
        {

            public RouterOutput Output { get; private set; }
            public RouterInput Input { get; private set; }

            public Crosspoint(RouterOutput output, RouterInput input)
            {
                Output = output;
                Input = input;
            }
            
            public void Take()
            {
                Output.Crosspoint = Input;
            }

        }

    }

}
