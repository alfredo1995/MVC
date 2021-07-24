crate new projetc > criar uma Solução Vazia  > Solution AppFuncional

        Seleciona a Soluiton AppFuncional >  criar um novo projeto > ASP.NET Web Application (.NET Framework)


Setup da do novo projeto ASP.NET Web Application (.NET Framework) entro da solution AppFuncional

        Nome AppMvc >dentro Repos > dentro AppFuncional > ASP.NET Web Application > MVC > CHANGE > Individual user acounts

Setando as migrations automatica> 
	AppFuncional>Migrations> Configurations.cs
	alterar AutomaticMigrationsEnabled de false para true;
	  public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }


Disponibilizando banco de dados / Habilitando as migrations
        
        packge manager console> enable-migrations            
        packge manager console> Update-Database -Verbose -force     

        
Criar o Scafolding da Aplicação

       AppFuncional>AppMvc>Models> Criar class Aluno.cs

       
Class Aluno.cs>

       namespace AppMvc.Models

       {
       public class Aluno
       {
       }
       }
    
Atribuir as propriedades da classe Aluno.Cs >
                  
   
              
        using System;
        using System.ComponentModel;
        using System.ComponentModel.DataAnnotations;

        namespace AppMvc.Models
        {
            public class Aluno 
            {
                [Key]  
                public int id { get; set; }
            
                [DisplayName("Nome Completo")]
                [Required(ErrorMessage ="o campo {0} ")]
                [MaxLength(100, ErrorMessage = "no max 100 caracteres ")]
                public string  Nome { get; set; }

                [DisplayName("Email")]
                [Required(ErrorMessage = "o campo {0} ")]
                [MaxLength(100, ErrorMessage = "O email e invalido ")]
                public string Email { get; set; }

                [Required(ErrorMessage = "o campo {0} ")]
                public string CPF { get; set; }

                public DateTime DataMatricula { get; set; }
                public bool Ativo { get; set; }

            }
        }


        crtl + shift + b = copilar para execuçoes dos proximos passos

Criar o Crud de Alunos 

        
        AppFuncional>AppMvc>Controller> Criar Controller> Escolher MVC 5 Controller With Views, Using Entity Framework > ADD 
        
        Configurar a controller > Model class : Aluno (AppMvc.Models) Data COntext class: ApplicationDbContext (AppMvc.Models) 

        Nome da controlle : AlunosController > add 
        

Adicionar o DbSet na  AppFuncional>AppMVC>Models>IdentityModels.cs


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        //Adicionar o DbSet
        public DbSet<Aluno> Alunos { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Aluno>().ToTable("Alunos");
            base.OnModelCreating(modelBuilder);
        } }
        

        crtl + shift + b 

        Packge manager Console> Update-Database -Verbose 


        Adicionador Route link 

        >Viwe>Shared> Layout.cs.html          <li>@Html.ActionLink("Alunos", "Index", "Alunos")</li>  
