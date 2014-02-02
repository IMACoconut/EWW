﻿using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class StreetGenerator {
    List<StreetScript> placed = new List<StreetScript>();
    List<StreetScript> open = new List<StreetScript>();
	int maxRadius;
	
	StreetScript getRandomStreet(StreetScript[] streets, int minPath, int maxPath) {
		ArrayList tmp = new ArrayList();
		foreach(StreetScript s in streets) {
			if(s.getPathsCount() >= minPath && s.getPathsCount() <= maxPath) {
				tmp.Add(s);	
			}
		}
		
		int r = Random.Range(0, tmp.Count-1);
		StreetScript ret = StreetScript.Instantiate((StreetScript)tmp[r]) as StreetScript;
		if(ret == null)
			Debug.Log("generating null street!");
		return ret;
	}
	
	bool isOpen(int x, int y) {
		foreach(StreetScript s in placed) {
			if(s == null)
				continue;
		
			if(s.x == x && s.y == y) {
				if(s.E && !exists (x, y-1))
					return true;
				if(s.W && !exists (x, y+1))
					return true;
				if(s.N && !exists (x+1, y))
					return true;
				if(s.S && !exists (x-1, y))
					return true;
			}
		}
		return false;
	}
	
	bool exists(int x, int y) {
		foreach(StreetScript s in placed) {
			if(s == null)
				continue;
			if(s.x == x && s.y == y)
				return true;
		}
		
		return false;
	}
	
	void placeStreet(StreetScript[] streets) {
		StreetScript s = open[0] as StreetScript;
		StreetScript s2 = null;
		int x2 = s.x;
		int y2 = s.y;

        //int min = (maxRadius > 2 && System.Math.Sqrt(x2*x2 + y2*y2) > 3.0) ? 1 : 2;

		if(s.W && !exists (s.x,s.y+1))
		{
			y2 = y2+1;
			if(y2 >= maxRadius)
				s2 = getRandomStreet(streets, 1,1);
			else
				s2 = getRandomStreet(streets, 1,4);
			while(!s2.E)
				s2.rotateLeft();
		} else if(s.E && !exists (s.x, s.y-1)) {
			y2 = y2-1;
			if(y2 <= -maxRadius)
				s2 = getRandomStreet(streets, 1,1);
			else
				s2 = getRandomStreet(streets, 1,4);
			while(!s2.W)
				s2.rotateLeft();
		} else if(s.N && !exists (s.x+1, s.y)) {
			x2 = x2+1;
			if(x2 >= maxRadius)
				s2 = getRandomStreet(streets, 1,1);
			else
				s2 = getRandomStreet(streets, 1,4);
			while(!s2.S)
				s2.rotateLeft();
		} else if(s.S && !exists (s.x-1, s.y)) {
			x2 = x2-1;
			if(x2 <= -maxRadius)
				s2 = getRandomStreet(streets, 1,1);
			else
				s2 = getRandomStreet(streets, 1,4);
			
			while(!s2.N)
				s2.rotateLeft();
		} else {
			open.Remove(s);
			return;
		}
		
		s2.x = x2;
		s2.y = y2;
		placed.Add(s2);
		if(isOpen(x2, y2))
			open.Add(s2);
		else
			s2.open = false;
		
		if(!isOpen (s.x,s.y)) {
			open.Remove(s);
			s.open = false;
		}
	}

    public List<StreetScript> Generate(GameScript game, StreetScript[] streets, int radius)
    {
        /*Random.seed = 1039656428;*/
        /* Random.seed = -331318947;*/
        // Random.seed = -2098638175; 

		//Random.seed = 508101336;
        Debug.Log("Seed: " + Random.seed);
        
		placed.Clear();
		open.Clear();
		maxRadius = radius;
		
		StreetScript first = getRandomStreet(streets, 2, 3);
		first.x = 0; first.y = 0;
		placed.Add(first);
		open.Add(first);
		int tmp = 0;
		while(open.Count > 0) {
			placeStreet(streets);
			tmp++;
		}

        foreach (StreetScript s in placed)
        {

            s.transform.position = new Vector3(25.6f * 3 * s.x * Constants.cityScale, 0, 25.6f * 3 * s.y * Constants.cityScale);
            s.transform.localScale *= Constants.cityScale; 
        }
            

		game.currentValve = ValveScript.Instantiate(game.valve) as ValveScript;
		game.currentValve.useEnabled = true;
        game.currentValve.transform.localScale *= Constants.cityScale;
        GameObject[] valveAttach = GameObject.FindGameObjectsWithTag("ValvePlace");
        int val = Random.Range(1, valveAttach.Length - 1); 
        placeValve(game, valveAttach, val );

		game.currentDoor = DoorScript.Instantiate(game.door) as DoorScript;
		game.currentDoor.locked = true;

        GameObject[] doorAttach = GameObject.FindGameObjectsWithTag("Attachment");
        placeDoor(game, doorAttach, Random.Range(1, doorAttach.Length-1));

		return placed;
	}
	
	bool placeValve(GameScript game, GameObject[] valves, int pos) {
		Transform a = valves[pos].transform;
		game.currentValve.transform.position = a.position;
		game.currentValve.transform.rotation = a.rotation;
		return true;
	}
	
	bool placeDoor(GameScript game, GameObject[] valves, int pos) {
		Transform a = valves[pos].transform;
		game.currentDoor.transform.position = a.position;
		game.currentDoor.transform.rotation = a.rotation;
       
		return true;
	}
}
