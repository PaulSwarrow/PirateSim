namespace App.Character
{
    public class CharacterRootMotionMotor : CharacterMotor
    {
        public static CharacterMotor Create() => new CharacterRootMotionMotor();
        protected override void OnEnable()
        {
            agent.view.MoveEvent += OnAnimatorMove;
            
        }

        public override void Update()
        {
        }

        protected override void OnDisable()
        {
            agent.view.MoveEvent -= OnAnimatorMove;
            
        }

        private void OnAnimatorMove()
        {
            agent.view.transform.position += agent.view.deltaPosition;
            agent.view.transform.rotation *= agent.view.deltaRotation;
        }
    }
}