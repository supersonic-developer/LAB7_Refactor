using OpenCvSharp;

namespace TurkMite
{
    public class StateTurkMite : TurkmiteBase
    {
        readonly public Vec3b black = new Vec3b(0, 0, 0);
        readonly public Vec3b white = new Vec3b(255, 255, 255);
        readonly public Vec3b red = new Vec3b(0, 0, 255);
        readonly public AState aState;
        readonly public BState bState;
        readonly public CState cState;

        private StateBase currentState;

        public override int PreferredIterationCount { get { return 50000; } }
        public StateBase CurrentState 
        {
            get { return currentState; }
            set 
            { 
                currentState = value;
                currentState.Enter();
            } 
        }

        public StateTurkMite(Mat image) : base(image) 
        {
            aState = new AState(this);
            bState = new BState(this);
            cState = new CState(this);
            currentState = aState;
        }

        public override void Step()
        {
            int deltaDirection;
            bool isStep;
            (indexer[y, x], deltaDirection, isStep) = currentState.GetColorAndDirectionAndStep(indexer[y, x]);
            if (isStep)
                PerformMove(deltaDirection);
        }

        protected override (Vec3b newColor, int deltaDirection) GetNextColorAndUpdateDirection(Vec3b currentColor)
        {
            return (this.black, 0);
        }
    }

    public abstract class StateBase
    {
        protected readonly StateTurkMite turkmite;

        public StateBase(StateTurkMite turkmite) { this.turkmite = turkmite; }

        public virtual void Enter() { return; }
        public virtual (Vec3b newColor, int deltaDirection, bool isStep) GetColorAndDirectionAndStep(Vec3b currentColor) { return (currentColor, 0, true); }
        
    }

    public class AState : StateBase
    {
        private int concernedBlackPixels = 0;
        public AState(StateTurkMite turkmite) : base(turkmite) { }

        public override void Enter()
        {
            base.Enter();
            concernedBlackPixels = 0;
        }

        public override (Vec3b newColor, int deltaDirection, bool isStep) GetColorAndDirectionAndStep(Vec3b currentColor)
        {
            if (currentColor != turkmite.black)
            {
                return base.GetColorAndDirectionAndStep(currentColor);
            }
            concernedBlackPixels++;
            if (concernedBlackPixels == 3)
            {
                turkmite.CurrentState = turkmite.bState;
                return (turkmite.white, -2, false);
            }
            return (turkmite.black, 0, true);
        }


    }

    public class BState : StateBase
    {
        public BState(StateTurkMite turkmite) : base(turkmite) { }

        public override (Vec3b newColor, int deltaDirection, bool isStep) GetColorAndDirectionAndStep(Vec3b currentColor)
        {
            if (currentColor == turkmite.black)
            {
                return (turkmite.white, -1, true);
            }
            else if (currentColor == turkmite.white)
            {
                return (turkmite.red, 1, true);
            }
            else
            {
                turkmite.CurrentState = turkmite.cState;
                return (turkmite.black, 0, true);
            }
        }
    }

    public class CState : StateBase
    {
        private int steps = 0;
        private bool isEnter = false;
        public CState(StateTurkMite turkmite) : base(turkmite) { }

        public override void Enter()
        {
            base.Enter();
            isEnter = true;
        }

        public override (Vec3b newColor, int deltaDirection, bool isStep) GetColorAndDirectionAndStep(Vec3b currentColor)
        {
            if (isEnter)
            {
                isEnter = false;
                return (turkmite.red, 0, true);
            }
            else 
            {
                steps++;
                if (steps == 5)
                {
                    if (currentColor == turkmite.red)
                    { turkmite.CurrentState = turkmite.bState; }
                    else
                    { turkmite.CurrentState = turkmite.aState; }
                }
                if (currentColor == turkmite.black)
                { return (turkmite.white, -1, true); }
                else if (currentColor == turkmite.white)
                { return (turkmite.red, 1, true); }
                else
                {
                    turkmite.CurrentState = turkmite.cState;
                    return (turkmite.black, -1, false);
                }
            }
        }
    }
}
