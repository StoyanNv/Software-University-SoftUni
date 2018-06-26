namespace SimpleMvc.App.Controllers
{
    using BindingModels;
    using Data;
    using Domain;
    using Framework.Attributes.Methods;
    using Framework.Controllers;
    using Framework.Interfaces;
    using Framework.Interfaces.Generics;
    using System.Collections.Generic;
    using System.Linq;
    using ViewModels;

    public class UsersController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserBindingModel model)
        {
            User user = new User()
            {
                Username = model.Username,
                Password = model.Password
            };
            using (var context = new NotesDbContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
            return View();
        }

        [HttpGet]
        public IActionResult<AllUsernamesViewModel> All()
        {
            Dictionary<int, string> usernames = null;

            using (var context = new NotesDbContext())
            {
                usernames = context.Users.ToDictionary(x => x.Id, y => y.Username);
            }
            AllUsernamesViewModel viewModel = new AllUsernamesViewModel()
            {
                Usernames = usernames
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult<UserProfileViewModel> Profile(int id)
        {
            User user = null;
            IList<Note> notes;

            using (var context = new NotesDbContext())
            {
                user = context.Users.FirstOrDefault(u => u.Id == id);
                notes = context.Notes.Where(n => n.User.Id == id).ToList();
            }
            UserProfileViewModel userProfile = new UserProfileViewModel()
            {
                UserId = id,
                Username = user.Username,
                Notes = notes
                .Select(n => new NoteViewModel()
                {
                    Title = n.Title,
                    Content = n.Content
                }
                )
            };
            return View(userProfile);
        }

        [HttpPost]
        public IActionResult<UserProfileViewModel> Profile(AddNoteBindingModel model)
        {
            using (var context = new NotesDbContext())
            {
                var user = context.Users.Find(model.UserId);

                var note = new Note()
                {
                    Title = model.Title,
                    Content = model.Content
                };
                user.Notes.Add(note);
                context.SaveChanges();
            }
            return Profile(model.UserId);
        }
    }
}