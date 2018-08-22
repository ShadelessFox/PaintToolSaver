using System;
using System.Timers;

namespace SAI_Autosaver.Core
{
    enum TimerState
    {
        NeverSaved = 1,
        SavingDisabled,
        SavingEnabled,
        WaitingForCanvas,
        WaitingForProcess,
        WaitingForProject
    }

    class DelayTimer
    {
        public event EventHandler<TimerState> OnNewState;
        public event EventHandler<int> OnProgress;
        public event EventHandler OnNotSavedProjectNotify;

        public TimerState LastState { get; set; }
        public bool NotifiedIfProjectNeverSaved { get; set; }

        private int currentTime;

        public int CurrentTime
        {
            get
            {
                return currentTime;
            }
            set
            {
                currentTime = value;
                OnProgress?.Invoke(this, value);
            }
        }

        public int MaximumTime { get; set; }

        private Timer timer;

        public DelayTimer(int interval)
        {
            timer = new Timer(interval);
            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (Properties.Settings.Default.BackupEnabled)
            {
                if (SaiHelper.ObtainProcess())
                {
                    if (SaiHelper.IsProjectOpened())
                    {
                        if (SaiHelper.IsProjectHasPath())
                        {
                            if (SaiHelper.IsProjectModified())
                            {
                                UpdateState(TimerState.SavingEnabled);

                                if (CurrentTime < MaximumTime)
                                {
                                    CurrentTime++;
                                }
                                else
                                {
                                    SaiHelper.SaveProject();

                                    if (Properties.Settings.Default.BackupIntoFolder)
                                    {
                                        SaiHelper.CopyProjectFileInto(Properties.Settings.Default.BackupFolderPath);
                                    }

                                    CurrentTime = 0;
                                }
                            }
                            else
                            {
                                UpdateState(TimerState.WaitingForCanvas);
                            }
                        }
                        else
                        {
                            UpdateState(TimerState.NeverSaved);

                            if (Properties.Settings.Default.BackupNotifyNotSaved && !NotifiedIfProjectNeverSaved)
                            {
                                if (CurrentTime < MaximumTime)
                                {
                                    CurrentTime++;
                                }
                                else
                                {
                                    OnNotSavedProjectNotify?.Invoke(this, EventArgs.Empty);
                                    NotifiedIfProjectNeverSaved = true;
                                    CurrentTime = 0;
                                }
                            }
                        }
                    }
                    else
                    {
                        UpdateState(TimerState.WaitingForProject);
                    }
                }
                else
                {
                    UpdateState(TimerState.WaitingForProcess);
                }
            }
            else
            {
                UpdateState(TimerState.SavingDisabled);
            }
        }

        private void UpdateState(TimerState newState)
        {
            if (LastState == newState)
            {
                return;
            }

            LastState = newState;

            if (newState != TimerState.SavingEnabled)
            {
                CurrentTime = 0;
            }

            if (newState == TimerState.WaitingForProject)
            {
                NotifiedIfProjectNeverSaved = false;
            }

            OnNewState?.Invoke(this, newState);
        }
    }
}
