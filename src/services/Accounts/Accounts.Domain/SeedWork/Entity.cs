namespace Accounts.Domain.SeedWork
{
    public class Entity
    {
        private int _id;

        public virtual int Id
        {
            get
            {
                return _id;
            }
            protected set
            {
                _id = value;
            }
        }
    }
}