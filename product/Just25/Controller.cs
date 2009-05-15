using System;
using System.Runtime.InteropServices;

using iTunesLib;

namespace Just25
{
    public class Controller : IDisposable
    {
        private iTunesApp m_App;

        /// <summary>
        /// Gets a value indicating whether [app class exists].
        /// </summary>
        /// <value><c>true</c> if [app class exists]; otherwise, <c>false</c>.</value>
        /// <remarks>This is really only for testing.</remarks>
        public bool AppClassExists
        {
            get
            {
                return m_App != null;
            }
        }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>The state.</value>
        public ITPlayerState State
        {
            get
            {
                return m_App.PlayerState;
            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Controller"/> class.
        /// </summary>
        public Controller()
            : this(new iTunesAppClass())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Controller"/> class.
        /// </summary>
        /// <param name="app">The app.</param>
        public Controller(iTunesApp app)
        {
            m_App = app;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (Marshal.IsComObject(m_App))
            {
                while (Marshal.ReleaseComObject(m_App) > 0)
                {
                    // spin
                }
            }
        }

        /// <summary>
        /// Plays this instance.
        /// </summary>
        public void Play()
        {
            if (State != ITPlayerState.ITPlayerStatePlaying)
            {
                m_App.Play();
            }
        }

        /// <summary>
        /// Pauses this instance.
        /// </summary>
        public void Pause()
        {
            if (State == ITPlayerState.ITPlayerStatePlaying)
            {
                m_App.Pause();
            }
        }

        /// <summary>
        /// Plays the pause.
        /// </summary>
        public void PlayPause()
        {
            m_App.PlayPause();
        }

        /// <summary>
        /// Quits this instance.
        /// </summary>
        public void Quit()
        {
            m_App.Quit();
        }


    }
}