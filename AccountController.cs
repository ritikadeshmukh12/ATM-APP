
public class AccountController : Controller {
    private readonly ATMContext _context;

    public AccountController(ATMContext context) {
        _context = context;
    }

    public IActionResult Login() => View();

    [HttpPost]
    public IActionResult Login(string username, string password) {
        var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        if (user != null) {
            HttpContext.Session.SetInt32("UserId", user.Id);
            return RedirectToAction("Index", "ATM");
        }
        ViewBag.Error = "Invalid credentials";
        return View();
    }
}
