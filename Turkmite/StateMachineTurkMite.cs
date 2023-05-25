﻿using OpenCvSharp;
using System;

namespace TurkMite
{
    public class StateTurkMite : TurkmiteBase
    {
        readonly public AState aState;
        readonly public BState bState;
        readonly public CState cState;

        private StateBase currentState;

        public override int PreferredIterationCount { get { return 50000; } }
        public StateBase CurrentState 
        {
            get { return currentState; }
            private set 
            { 
                currentState = value;
                currentState.Enter();
            } 
        }

        public StateTurkMite(Mat image) : base(image) 
        {
            Action<StateBase> a = (StateBase s) => { CurrentState = s; };
            aState = new AState(this, a);
            bState = new BState(this, a);
            cState = new CState(this, a);
            currentState = aState;
        }

        protected override (Vec3b newColor, int deltaDirection) GetNextColorAndUpdateDirection(Vec3b currentColor)
        {
            return currentState.GetColorAndDirection(currentColor);
        }
    }

    public abstract class StateBase
    {
        protected readonly StateTurkMite turkmite;
        readonly protected Vec3b black = new Vec3b(0, 0, 0);
        readonly protected Vec3b white = new Vec3b(255, 255, 255);
        readonly protected Vec3b red = new Vec3b(0, 0, 255);
        protected Action<StateBase> changeState;
        public StateBase(StateTurkMite turkmite, Action<StateBase> changeState) 
        { 
            this.turkmite = turkmite;
            this.changeState = changeState;
        }

        public virtual void Enter() { return; }
        public virtual (Vec3b newColor, int deltaDirection) GetColorAndDirection(Vec3b currentColor) { return (currentColor, 0); }
    }

    public class AState : StateBase
    {
        private int concernedBlackPixels = 0;
        public AState(StateTurkMite turkmite, Action<StateBase> changeState) : base(turkmite, changeState) { }

        public override void Enter()
        {
            base.Enter();
            concernedBlackPixels = 0;
        }

        public override (Vec3b newColor, int deltaDirection) GetColorAndDirection(Vec3b currentColor)
        {
            if (currentColor != black)
            {
                return base.GetColorAndDirection(currentColor);
            }
            concernedBlackPixels++;
            if (concernedBlackPixels == 3)
            {
                changeState(turkmite.bState);
                return (white, -2);
            }
            return (black, 0);
        }
    }

    public class BState : StateBase
    {
        public BState(StateTurkMite turkmite, Action<StateBase> changeState) : base(turkmite, changeState) { }

        public override (Vec3b newColor, int deltaDirection) GetColorAndDirection(Vec3b currentColor)
        {
            if (currentColor == black)
            {
                return (white, -1);
            }
            else if (currentColor == white)
            {
                return (red, 1);
            }
            else
            {
                changeState(turkmite.cState);
                return (black, 0);
            }
        }
    }

    public class CState : StateBase
    {
        private int steps = 0;
        private bool isEnter = false;
        public CState(StateTurkMite turkmite, Action<StateBase> changeState) : base(turkmite, changeState) { }

        public override void Enter()
        {
            base.Enter();
            isEnter = true;
        }

        public override (Vec3b newColor, int deltaDirection) GetColorAndDirection(Vec3b currentColor)
        {
            steps++;
            if (isEnter)
            {
                isEnter = false;
                return (red, 0);
            }
            else 
            {
                if (steps == 5)
                {
                    if (currentColor == red)
                    { changeState(turkmite.bState); }
                    else
                    { changeState(turkmite.aState); }
                }
                if (currentColor == black)
                { return (white, -1); }
                else if (currentColor == white)
                { return (red, 1); }
                else
                {
                    changeState(turkmite.cState);
                    return (black, -1);
                }
            }
        }
    }
}
