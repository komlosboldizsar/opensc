using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs
{
    internal class UmdPeriodicUpdater
    {

        public static void Start() => Task.Run(taskMethod);

        private static async Task taskMethod()
        {
            while (true)
            {
                tick();
                await Task.Delay(1000);
            }
        }

        private static void tick()
        {
            foreach (Umd umd in UmdDatabase.Instance)
            {
                umd.secondsSinceLastPeriodicUpdate++;
                if (umd.PeriodicUpdateEnabled && (umd.secondsSinceLastPeriodicUpdate >= umd.PeriodicUpdateInterval))
                {
                    umd.UpdateEverything();
                    umd.secondsSinceLastPeriodicUpdate = 0;
                }
            }
        }

    }
}
