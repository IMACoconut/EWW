using System;

	public class CTimer : System.Timers.Timer
    {
        private DateTime m_dueTime;

        public CTimer() : base()
        {
            this.Elapsed += this.ElapsedAction;
        }

        protected new void Dispose()
        {
            this.Elapsed -= this.ElapsedAction;
            base.Dispose();
        }

        public TimeSpan TimeLeft
        {
            get
            {
                return (this.m_dueTime - DateTime.Now);
            }
        }
	
		public void AddTime(float millisecs) {
			m_dueTime = m_dueTime.AddMilliseconds(millisecs);
		}

        public new void Start()
        {
            this.m_dueTime = DateTime.Now.AddMilliseconds(this.Interval);
            base.Start();
        }

        private void ElapsedAction(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (this.AutoReset)
            {
                this.m_dueTime = DateTime.Now.AddMilliseconds(this.Interval);
            }
        }
    }

