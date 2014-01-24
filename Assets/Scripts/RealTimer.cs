using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using UnityEngine;

public class RealTimer
{
    private DateTime m_time;
    private TimeSpan m_elapsed;
    public int delay = 1000;
    public bool paused;
    public bool done;
    public ElapsedEventHandler elapsed;

    public void Start() 
    {
        done = false;
        m_time = DateTime.Now;
        paused = false;
        m_elapsed = TimeSpan.Zero;
    }

    public void Update()
    {
        if (Constants.pause)
            return;
        if (!done && TimeLeft().CompareTo(TimeSpan.Zero) < 0) 
        {
            elapsed.Invoke(this, null);
        }

    }

    public void Stop()
    {
        done = true;
        m_time = DateTime.Now;
        paused = false;
        m_elapsed = TimeSpan.Zero;
    }

    public bool IsPaused()
    {
        return paused;
    }

    public void Pause()
    {
        if (!paused)
        {
            m_elapsed += DateTime.Now - m_time;
            paused = true;
        }
    }

    public void Resume()
    {
        if (paused)
        {
            m_time = DateTime.Now;
            paused = false;
        }
    }

    public TimeSpan TimeElapsed()
    {
        if (done)
            return TimeSpan.Zero;

        if (!paused)
        {
            DateTime now = DateTime.Now;
            return m_elapsed + (now - m_time);
        }
        else
        {
            return m_elapsed;
        }
    }

    public TimeSpan TimeLeft()
    {
        if (done)
            return TimeSpan.Zero;

        TimeSpan tmp = new TimeSpan(0,0,0,0,delay);
        return tmp - TimeElapsed();
        /*DateTime tmp = m_time;
        if (!paused)
        {
            return tmp.AddMilliseconds(delay) - DateTime.Now;
        }
        else
        {
            return tmp.AddMilliseconds(delay) - (m_time + m_elapsed);
        }*/
    }
}

