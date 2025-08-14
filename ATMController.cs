public class ATMController : Controller {
    private readonly ATMContext _context;

    public ATMController(ATMContext context) {
        _context = context;
    }

    public IActionResult Index() {
        int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
        var user = _context.Users.Find(userId);
        return View(user);
    }

    public IActionResult Deposit() => View();

    [HttpPost]
    public IActionResult Deposit(decimal amount) {
        int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
        var user = _context.Users.Find(userId);
        user.Balance += amount;
        _context.Transactions.Add(new Transaction { UserId = userId, Date = DateTime.Now, Type = "Deposit", Amount = amount });
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Withdraw() => View();

    [HttpPost]
    public IActionResult Withdraw(decimal amount) {
        int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
        var user = _context.Users.Find(userId);
        if (user.Balance >= amount) {
            user.Balance -= amount;
            _context.Transactions.Add(new Transaction { UserId = userId, Date = DateTime.Now, Type = "Withdraw", Amount = amount });
            _context.SaveChanges();
        } else {
            ViewBag.Error = "Insufficient balance";
        }
        return RedirectToAction("Index");
    }

    public IActionResult History() {
        int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
        var transactions = _context.Transactions.Where(t => t.UserId == userId).ToList();
        return View(transactions);
    }
}

