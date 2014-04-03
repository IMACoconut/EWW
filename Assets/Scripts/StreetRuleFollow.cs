using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    class StreetRuleFollow : StreetRule
    {
        private double gaussian(float x, float y, float x0, float y0, float A, float sigx, float sigy)
        {
            return A * Math.Exp(-(Math.Pow(x - x0,2) / (2 * Math.Pow(sigx,2)) + (Math.Pow(y - y0,2) / (2 * Math.Pow(sigy,2)))));
        }
        
        private double linear(float x, float y, float x0, float y0, float A, float d)
        {
            return Math.Max(A * ((d - Math.Sqrt(Math.Pow(x - x0, 2) + Math.Pow(y - y0, 2))) / d), 0);
        }
        public override void setRuleOnStreets(GameScript game, List<StreetScript> streets, Vector3 valve, Vector3 door)
        {
            Debug.Log("Set rule");
            valve += new Vector3(64.0f,64.0f,64.0f);
            valve /= 128.0f;
            valve.x = (float)Math.Floor(valve.x);
            valve.y = (float)Math.Floor(valve.y);
            valve.z = (float)Math.Floor(valve.z);

            const float alpha = 10.0f;
            Debug.Log("Streets: " + streets.Count);

            foreach (StreetScript s in streets)
            {
                /*float dist = (valve - s.position).magnitude;
                Debug.Log(valve);
                Debug.Log(s.position);
                Debug.Log(dist);*/
                List<Transform> affiches = getAffiches(s);
                float factor = (float)linear(s.position.x, s.position.z, valve.x, valve.z, 1f, 1.5f);
                Debug.Log("factor: " + factor);
                foreach (Transform t in affiches)
                {
                    //float factor = (float)linear(t.position.x, t.position.z, valve.x, valve.z, 0.8f, 3f);
                    float rand = UnityEngine.Random.Range(0.0f, 1.0f);
                    if (rand > factor)
                        continue;

                    GameObject poster = game.getRandomPoster();
                    poster.transform.position = t.position;
                    poster.transform.rotation = t.rotation;
                    s.affiches.Add(poster);

                    // Random rotation pour que les posters ne soient pas tous dans le même sens
                    float r = UnityEngine.Random.Range(-30, 30);
                    poster.transform.Rotate(new Vector3(0, 0, 1), r);
                    float x = UnityEngine.Random.Range(-1.0f, 1.0f);
                    float y = UnityEngine.Random.Range(-1.0f, 1.0f);
                    poster.transform.Translate(new Vector3(x, y, 0));
                }
            }
        }
    }
}
