using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace gj4thFeb2012
{
    public class Timer
    {
        private int _lengthMs;
        private int _currentMs;
        private bool _running;

        public delegate void CallbackEvent();

        private CallbackEvent _callback;

        public Timer(int lengthMs, CallbackEvent callback)
        {
            _lengthMs = lengthMs;
            _currentMs = lengthMs;
            _callback = callback;
        }

        public void Update(GameTime gameTime)
        {
            int dt = gameTime.ElapsedGameTime.Milliseconds;

            if (!_running) return;

            _currentMs -= dt;
            if (_currentMs <= 0)
            {
                _currentMs = _lengthMs;
                _callback();
            }
        }

        public void Start()
        {
            _running = true;
        }

        public void Stop()
        {
            _running = false;
        }

        public void Reset()
        {
            _currentMs = _lengthMs;
        }

        public void ChangeLength(int newLength)
        {
            _lengthMs = newLength;
        }
    }
}
