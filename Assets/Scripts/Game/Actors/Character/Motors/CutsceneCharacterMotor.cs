namespace Game.Actors.Character.Motors
{
    public class CutsceneCharacterMotor : CharacterMotor
    {
        public static CharacterMotor Create() => new CutsceneCharacterMotor();
        protected override void OnEnable()
        {
            Actor.view.MoveEvent += OnAnimatorMove;
            
        }

        public override void Update()
        {
        }

        protected override void OnDisable()
        {
            Actor.view.MoveEvent -= OnAnimatorMove;
            
        }

        private void OnAnimatorMove()
        {
            Actor.view.transform.position += Actor.view.deltaPosition;
            Actor.view.transform.rotation *= Actor.view.deltaRotation;
        }
    }
}