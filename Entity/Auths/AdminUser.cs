using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Auths
{
    public class AdminUser 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string UserNumber { get; set; }
        public Guid UserGuidNumber { get; set; }

        public bool Status { get; set; }

        public UserType UserType { get; set; }

        public Guid AdminGuidNumber { get; set; } = Guid.NewGuid();

        public AdminRole Role { get; set; } = AdminRole.StandardAdmin; 

        public bool IsActive { get; set; } = true; 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("CreatedByAdmin")]
        public int? CreatedByAdminId { get; set; } 
        public virtual AdminUser CreatedByAdmin { get; set; } 

        [ForeignKey("UpdatedByAdmin")]
        public int? UpdatedByAdminId { get; set; } 
        public virtual AdminUser UpdatedByAdmin { get; set; } 
    }

    public enum AdminRole
    {
        SuperAdmin = 1,  
        StandardAdmin = 2 
    }
}
