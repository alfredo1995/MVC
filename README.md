MVC is used to create websites. In this case, controllers typically return a view (i.e., the HTML response) for browser requests.

	Web APis, on the other hand, are made to be consumed by other applications. If you want to allow other applications to access your data/functionality, you can create a Web API to facilitate this access. For example, Facebook has an API to allow application developers to access information about users using the application.

The main difference between the project types is that the MVC project type adds web-specific things like standard CSS, JavaScript files and other resources required for a website, which are not required for an API.

<br>  
  
crate new projetc > criar uma Solução Vazia  > Solution AppFuncional

        Seleciona a Soluiton AppFuncional >  criar um novo projeto > ASP.NET Web Application (.NET Framework)
  
 
Setup of the new ASP.NET Web Application (.NET Framework) project within the AppFuncional solution
 
        Nome AppMvc >dentro Repos > dentro AppFuncional > ASP.NET Web Application > MVC > CHANGE > Individual user acounts
 
Setting the migrations automatica>   

	AppFuncional>Migrations> Configurations.cs 
	alterar AutomaticMigrationsEnabled de false para true;
	  public Configuration() 
        { 
            AutomaticMigrationsEnabled = true;
        }   

Making database available / Enabling migrations

        packge manager console> enable-migrations            
        packge manager console> Update-Database -Verbose -force     
        
Create Application Scafolding

       AppFuncional>AppMvc>Models> Criar class Aluno.cs
       
Class Aluno.cs>

       namespace AppMvc.Models

       {
       public class Aluno
       {
       } 
       } 
    
Assign class properties Aluno.Cs >                   
              
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

Command to compile the application to perform the next steps

        crtl + shift + b 

Create o Crud de Alunos 
       
        AppFuncional>AppMvc>Controller> Criar Controller> Escolher MVC 5 Controller With Views, Using Entity Framework > ADD 
        
        Configurar a controller > Model class : Aluno (AppMvc.Models) Data COntext class: ApplicationDbContext (AppMvc.Models) 

        Nome da controlle : AlunosController > add 
        
Add o DbSet na  AppFuncional>AppMVC>Models>IdentityModels.cs

    using System.Data.Entity;
	using System.Data.Entity.ModelConfiguration.Conventions;
	using System.Security.Claims;
	using System.Threading.Tasks;
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;

	namespace AppMvc.Models
	{
	    // É possível adicionar dados do perfil do usuário adicionando mais propriedades na sua classe ApplicationUser
	    public class ApplicationUser : IdentityUser
	    {
		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
		{
		    // Observe que o authenticationType deve corresponder àquele definido em CookieAuthenticationOptions.AuthenticationType
		    var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
		    // Adicionar declarações de usuário personalizado aqui
		    return userIdentity;
		}
	    }

	    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	    {
		public ApplicationDbContext()
		    : base("DefaultConnection", throwIfV1Schema: false)
		{
		}

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
		}

		    }
	}


Command to compile the application to perform the next steps

        crtl + shift + b 


Updates the database to the last migration or a specified migration

        Packge manager Console> Update-Database -Verbose 

Configure route mapping by attributes

        app.start > RouteConfig.cs      =    routes.MapMvcAttributeRoutes();

        Viwes> Shared> Layout.cs.html   =    <li>@Html.ActionLink("Alunos", "Index", "Alunos")</li>  

Mapeando as rotas e o verbos https

        Controllers > Alunos.Controller.cs 

        [HttpPost]                             =  Metodo de Requisição
        [Route("Editar-Aluno/{id:int}")]       =  Caminho da URL acessado pelo metodos

Implementando as rotas e os metodos 
    
        namespace AppMvc.Controllers{

        public class AlunosController : Controller
        {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        [Route("Listar-Alunos")]
        public async Task<ActionResult> Index()
        {
            return View(await db.Alunos.ToListAsync());
        }

        [HttpGet]
        [Route("Listar-Detalhe/{id:int}")]
        public async Task<ActionResult> Details(int id)
        {            
            Aluno aluno = await db.Alunos.FindAsync(id);
            if (aluno == null)
            {
                return HttpNotFound();
            }
            return View(aluno);
        }

        [HttpGet]
        [Route("Novo-Aluno")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Novo-Aluno")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,Nome,Email,CPF,DataMatricula,Ativo")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                db.Alunos.Add(aluno);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(aluno);
        }

        [HttpGet]
        [Route("Editar-Aluno/{id:int}")]
        public async Task<ActionResult> Edit(int id)
        {
            Aluno aluno = await db.Alunos.FindAsync(id);
            if (aluno == null)
            {
                return HttpNotFound();
            }
            return View(aluno);
        }

        [HttpPost]
        [Route("Editar-Aluno/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,Nome,Email,CPF,DataMatricula,Ativo")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aluno).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(aluno);
        }

        [HttpGet]
        [Route("Excluir-Aluno/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            Aluno aluno = await db.Alunos.FindAsync(id);
            if (aluno == null)
            {
                return HttpNotFound();
            }
            return View(aluno);
        }

        [HttpPost, ActionName("Delete")]        
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Aluno aluno = await db.Alunos.FindAsync(id);
            db.Alunos.Remove(aluno);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }  }   }
        

Customizações visuais

       Views > Alunos > Index.cshtml

Customizar Nome e o metodo de acesso da rota > Index.cshtml

       <h2>Matricula de Alunos</h2>

      <p>
         <a href="@Url.Action("Create")" class="btn btn-primary">Cadastro de Aluno</a>
      </p>
    
Customizar a tabela > Index.cshtml

       <td>
            <a href="@Url.Action("Edit", "Alunos", new { id = item.id })" class="btn btn-primary">Editar</a>
            <a href="@Url.Action("Details", "Alunos", new { id = item.id })" class="btn btn-success">Detalhe</a>
            <a href="@Url.Action("Delete", "Alunos", new { id = item.id })" class="btn btn-danger">Excluir</a>
      </td>
	
