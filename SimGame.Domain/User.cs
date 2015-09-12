using cb.core.domain;

namespace SimGame.Domain
{
    public class User : DomainObject
    {
        public override int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}