using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    class StreetRule : IStreetRule
    {
        virtual public IStreetRule.setRuleOnStreets(List<StreetScript> streets, Vector3 valve, Vector3 door);

        protected List<Transform> getAffiches(StreetScript street)
        {
            List<Transform> affiches = new List<Transform>();
            foreach (Transform c in s.gameObject.GetComponentsInChildren<Transform>())
                if (c.gameObject.tag.StartsWith("affiche"))
                    affiches.Add(c);

            System.Random rnd = new System.Random();
            affiches.OrderBy(x => rnd.Next());
            return affiches;
        }
    }
}
