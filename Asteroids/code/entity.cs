using SFML.Window;

namespace Asteroids
{
    //keeps all information needed for an entity on the screen
    class Entity
    {
        public Vector2f position;
        public Vector2f vector;
        public int angle;
        public float speed;
        public bool isVisible;

        public Entity()
        {
            position = new Vector2f(0, 0);
            vector = new Vector2f();
            angle = 0;
            speed = 0;
            isVisible = false;
        }

        public Entity(Vector2f startingPos)
        {
            position = startingPos;
            vector = new Vector2f(0, 0);
            angle = 0;
            speed = 0;
            isVisible = false;
        }
    }
}
