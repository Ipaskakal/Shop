using ConsoleEShopMultilayered.DAL.Models;
using ConsoleEShopMultilayered.BLL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using ConsoleEShopMultilayered.BLL.VievModels;

namespace ConsoleEShopMultilayered.PL
{
    /// <summary>
    /// Console user interface
    /// </summary>
    public class ConsoleUI
    {
        /// <summary>
        /// Current user
        /// </summary>
        public IRegisteredUser User { get; set; }

        /// <summary>
        /// Inputed string by user
        /// </summary>
        string userAction;

        /// <summary>
        /// Delegate for functions that outputs pages of console interface
        /// </summary>
        delegate void InterfacePages();
        /// <summary>
        /// Instance of delegate for functions that outputs pages of console interface
        /// </summary>
        private InterfacePages page;

        /// <summary>
        /// Field for connection with BLL
        /// </summary>
        IController Controller { get; set; }

        /// <summary>
        /// Constructor that sets user,controller and page
        /// </summary>
        public ConsoleUI(IRegisteredUser user, IController controller)
        {
            this.User = user;
            this.Controller = controller;
            this.page = MainPage;
        }

        /// <summary>
        /// Starts and keep console user interface
        /// </summary>
        public void Start()
        {
            while (this.page != null)
            {

                this.page.Invoke();
            }

        }

        /// <summary>
        /// Output main pagee and work with user actions
        /// </summary>
        public void MainPage()
        {

            this.Clear();
            if (this.User is RegisteredUser)
            {
                this.WriteRegisteredUserMainFunctions();
            }
            if (this.User is Administrator)
            {
                this.WriteAdministratorMainFunctions();
            }
            if (this.User == null)
            {
                this.WriteGuestMainFunctions();
            }

            this.userAction = this.GetUserText();
            if (userAction.Equals("show"))
                this.page = GetListOfAllProducts;
            else if (userAction.Equals("find"))
                this.page = FindProductByName;
            else if (userAction.Equals("sign up") && this.User == null)
                this.page = SignUp;
            else if (userAction.Equals("sign in") && this.User == null)
                this.page = SignIn;
            else if (userAction.Equals("watch history") && this.User is RegisteredUser)
                this.page = OrderHistory;
            else if (userAction.Equals("persinf") && this.User is RegisteredUser)
                this.page = PersonalInformationOfCurrentUser;
            else if (userAction.Equals("new product") && this.User is Administrator)
                this.page = CreateNewProduct;
            else if (userAction.Equals("show users") && this.User is Administrator)
                this.page = ListOfUsers;
            else if (userAction.Equals("0") && this.User != null)
                this.page = SignOff;
        }

        /// <summary>
        /// Set current user as guest and sends to main page
        /// </summary>
        private void SignOff()
        {
            this.User = null;
            this.page = MainPage;
        }

        /// <summary>
        /// Get fields from user and try to sign up new user in system
        /// </summary>
        private void SignUp()
        {
            RegisterUserViewModel userViewModel = new RegisterUserViewModel();
            while (!ConsoleUI.IsValidEmail(userViewModel.Email))
            {
                Console.WriteLine("Email :");
                userViewModel.Email = this.GetUserText();
            }
            while (!Regex.IsMatch(userViewModel.UserName, @"(\d|\w){6,12}"))
            {
                Console.WriteLine("UserName :");
                userViewModel.UserName = this.GetUserText();
            }
            while (!Regex.IsMatch(userViewModel.Password, @"(\d|\w){6,12}"))
            {
                Console.WriteLine("Password :");
                userViewModel.Password = this.GetUserText();
            }
            while (!userViewModel.Password.Equals(userViewModel.ConfirmPassword))
            {
                Console.WriteLine("Confirm password :");
                userViewModel.ConfirmPassword = this.GetUserText();
            }
            var temp = this.Controller.UserController.SignUpUser(userViewModel);
            if (temp)
                this.page = SignIn;
            else
                this.page = MainPage;

        }

        /// <summary>
        /// Get fields from user and try to sign in system
        /// </summary>
        private void SignIn()
        {
            RegisterUserViewModel userViewModel = new RegisterUserViewModel();
            Console.WriteLine("         Sign in   ");
            Console.WriteLine("Email :");
            userViewModel.Email = this.GetUserText();

            Console.WriteLine("Password :");
            userViewModel.Password = this.GetUserText();

            var temp = this.Controller.UserController.SignInUser(userViewModel);
            this.User = temp;
            this.page = MainPage;

        }

        /// <summary>
        /// Output page with all users and work with user actions
        /// </summary>
        private void ListOfUsers()
        {
            this.Clear();
            List<UsersViewModel> users = this.Controller.UserController.GetListOfUsers();
            foreach (var user in users)
            {
                Console.WriteLine("User:");
                this.OutputUser(user);
                Console.WriteLine("User`s orders: \n");
                var orders = this.Controller.OrderController.GetOrdersOfUser(user.Email);
                foreach (var order in orders)
                    this.OutputOrder(order);
            }
            Console.WriteLine(@"change status {id}- change status of order with id");
            Console.WriteLine(@"{email} - change information of user with email");
            Console.WriteLine("main - go to main page");
            Console.WriteLine("0 - Exit");

            while (true)
            {
                this.userAction = this.GetUserText();
                if (Regex.IsMatch(userAction, @"^change status \d{1,10}$"))
                {
                    this.ChangeOrderStatus(int.Parse(userAction.Remove(0, 14)));
                    this.page = MainPage;
                    return;
                }
                else if (ConsoleUI.IsValidEmail(userAction))
                    this.ChangePersonalInformation(userAction);
                else if (userAction.Equals("main"))
                {
                    this.page = MainPage;
                    return;
                }
                else if (userAction.Equals("0") && this.User != null)
                {
                    this.page = SignOff;
                    return;
                }
            }
        }

        /// <summary>
        /// Output page with information about current user and work with user actions
        /// </summary>
        private void PersonalInformationOfCurrentUser()
        {
            if (this.User != null)
                this.OutputUser(this.Controller.UserController.GetUser(this.User.Email));
            Console.WriteLine(@"change - change information of user with Id");
            Console.WriteLine("main - go to main page");
            Console.WriteLine("0 - Exit");

            while (true)
            {
                this.userAction = this.GetUserText();
                if (Regex.IsMatch(userAction, @"^change$"))
                {
                    this.ChangePersonalInformation(this.User.Email);
                    return;
                }
                else if (userAction.Equals("main"))
                {
                    this.page = MainPage;
                    return;
                }
                else if (userAction.Equals("0") && this.User != null)
                {
                    this.page = SignOff;
                    return;
                }
            }
        }

        /// <summary>
        /// Output page with information about current user and change information in current users profile
        /// </summary>
        private void ChangePersonalInformation(string userEmail)
        {

            var user = this.Controller.UserController.GetUser(userEmail);
            if (user == null)
            {
                return;
            }
            var newUser = new UsersViewModel() { UserName = user.UserName, Email = user.Email, Password = user.Password };
            while (true)
            {
                this.OutputUser(newUser);
                Console.WriteLine("name - to change username");
                Console.WriteLine("email -to change email");
                Console.WriteLine("password - to change password");
                Console.WriteLine("update - to save changes");
                Console.WriteLine("main - go to main page");
                Console.WriteLine("0 - Exit");

                this.userAction = this.GetUserText();
                if (userAction.Equals("name"))
                {
                    this.userAction = this.GetUserText();
                    if (Regex.IsMatch(userAction, @"(\d|\w){6,12}"))
                    {
                        newUser.UserName = userAction;
                    }
                }
                else if (userAction.Equals("email"))
                {
                    this.userAction = this.GetUserText();
                    if (ConsoleUI.IsValidEmail(userAction))
                    {
                        newUser.Email = userAction;
                    }
                }
                else if (userAction.Equals("password"))
                {
                    this.userAction = this.GetUserText();
                    if (Regex.IsMatch(userAction, @"(\d|\w){6,12}"))
                    {
                        newUser.Password = userAction;
                    }
                }
                else if (userAction.Equals("update"))
                {
                    var update = this.Controller.UserController.UpdateUser(user, newUser);
                    if (update == null)
                        Console.WriteLine("Update failed. try another email");
                    else
                    {
                        if (user.Email.Equals(this.User.Email))
                            this.User = update;
                        this.page = MainPage;
                        return;
                    }


                }
                else if (userAction.Equals("main"))
                {
                    this.page = MainPage;
                    return;
                }
                else if (userAction.Equals("0") && this.User != null)
                {
                    this.page = SignOff;
                    return;
                }
            }
        }

        /// <summary>
        /// get products from BLL
        /// </summary>
        private void GetListOfAllProducts()
        {
            List<Product> products = Controller.ProductController.GetListOfProducts();
            this.ListOfProducts(products);
        }

        /// <summary>
        /// Get information from uset and try to find product by sequesting BLL
        /// </summary>
        private void FindProductByName()
        {
            Console.Clear();
            Console.WriteLine("         Find products by name");
            Console.WriteLine("Write part of name :");
            this.userAction = this.GetUserText();
            this.ListOfProducts(this.Controller.ProductController.FindProductsbyName(this.userAction));
        }

        /// <summary>
        /// Output page with information about products and work with user actions
        /// </summary>
        private void ListOfProducts(List<Product> products)
        {
            this.Clear();
            foreach (var product in products)
                this.OutputProduct(product);
            Console.WriteLine(@"order + {Id} - make an order");
            Console.WriteLine("main - go to main page");
            if (this.User != null)
                Console.WriteLine("0 - Exit");

            while (true)
            {
                this.userAction = this.GetUserText();
                if (Regex.IsMatch(userAction, @"^order \d*$"))
                {
                    if (this.User == null)
                    {
                        this.page = SignIn;
                        return;
                    }
                    else
                    {
                        this.CreateNewOrder(userAction.Remove(0, 6));
                        return;
                    }
                }
                else if (userAction.Equals("main"))
                {
                    this.page = MainPage;
                    return;
                }
                else if (userAction.Equals("0") && this.User != null)
                {
                    this.page = SignOff;
                    return;
                }
            }
        }


        /// <summary>
        /// Output page with creating form of new product and send product in BLL
        /// </summary>
        private void CreateNewProduct()
        {
            Product product = new Product();
            while (true)
            {
                this.OutputProduct(product);
                Console.WriteLine("name - to change username");
                Console.WriteLine("description -to change description");
                Console.WriteLine("price - to change price");
                Console.WriteLine("add - to add prodyct");
                Console.WriteLine("main - go to main page");
                Console.WriteLine("0 - Exit");

                this.userAction = this.GetUserText();
                if (userAction.Equals("name"))
                {
                    this.userAction = this.GetUserText();
                    product.Name = userAction;

                }
                else if (userAction.Equals("description"))
                {
                    this.userAction = this.GetUserText();
                    product.Description = userAction;

                }
                else if (userAction.Equals("price"))
                {
                    this.userAction = this.GetUserText();
                    if (Regex.IsMatch(userAction, @"^(\d){1,12}$"))
                    {
                        product.Price = int.Parse(userAction);
                    }
                }
                else if (userAction.Equals("add"))
                {
                    this.Controller.ProductController.AddProduct(product);
                    this.page = MainPage;
                    return;
                }
                else if (userAction.Equals("main"))
                {
                    this.page = MainPage;
                    return;
                }
                else if (userAction.Equals("0") && this.User != null)
                {
                    this.page = SignOff;
                    return;
                }
            }
        }



        /// <summary>
        /// Output page with information about current user orders and work with user actions
        /// </summary>
        private void OrderHistory()
        {
            this.Clear();
            var orders = this.Controller.OrderController.GetOrdersOfUser(this.User.Email);
            foreach (var order in orders)
                this.OutputOrder(order);
            Console.WriteLine(@"change status {id}- change status of order with id");
            Console.WriteLine("main - go to main page");
            Console.WriteLine("0 - Exit");

            while (true)
            {
                this.userAction = this.GetUserText();
                if (Regex.IsMatch(userAction, @"^change status \d{1,10}$"))
                {
                    this.ChangeOrderStatus(int.Parse(userAction.Remove(0, 14)));
                    this.page = MainPage;
                    return;
                }
                else if (userAction.Equals("main"))
                {
                    this.page = MainPage;
                    return;
                }
                else if (userAction.Equals("0") && this.User != null)
                {
                    this.page = SignOff;
                    return;
                }
            }
        }

        /// <summary>
        /// Get order from BLL and invoke functions to change order status basing on current user rights
        /// </summary>
        /// <param name="id">id of order</param>
        private void ChangeOrderStatus(int id)
        {
            var order = this.Controller.OrderController.GetOrderById(id);
            if (this.User is Administrator)
                this.ChangeOrderStatusByAdmin(order);
            else
                this.ChangeOrderStatusByUser(order);


        }

        /// <summary>
        /// Change status of order with admin rigths
        /// </summary>
        /// <param name="order">order to change status</param>
        private void ChangeOrderStatusByAdmin(OrderViewModel order)
        {
            bool isUpdated = false;
            while (true)
            {
                this.OutputOrder(order);
                if (order.State < OrderState.Paid)
                    Console.WriteLine("Paid - to change status to");
                if (order.State < OrderState.Sent)
                    Console.WriteLine("Sent - to change status to");
                if (order.State < OrderState.Completed)
                    Console.WriteLine("Completed - to change status to ");
                if (order.State < OrderState.CanceledByUser)
                    Console.WriteLine("CanceledByAdmin - to change status to");
                if (isUpdated)
                    Console.WriteLine("Update - to update status");
                Console.WriteLine("main - go to main page");
                Console.WriteLine("0 - Exit");

                this.userAction = this.GetUserText();

                if (userAction.Equals("Paid") && order.State < OrderState.Paid)
                {
                    order.State = OrderState.Paid;
                    isUpdated = true;
                }
                else if (userAction.Equals("Sent") && order.State < OrderState.Sent)
                {
                    order.State = OrderState.Sent;
                    isUpdated = true;
                }
                else if (userAction.Equals("Completed") && order.State < OrderState.Completed)
                {
                    order.State = OrderState.Completed;
                    isUpdated = true;
                }
                else if (userAction.Equals("CanceledByAdmin") && order.State < OrderState.CanceledByAdmin)
                {
                    order.State = OrderState.CanceledByAdmin;
                    isUpdated = true;
                }
                else if (userAction.Equals("Update") && isUpdated)
                {
                    this.Controller.OrderController.UpdateStatebyId(order.Id, order.State);
                    this.page = MainPage;
                    return;
                }

                if (userAction.Equals("main"))
                {
                    this.page = MainPage;
                    return;
                }
                else if (userAction.Equals("0") && this.User != null)
                {
                    this.page = SignOff;
                    return;
                }
            }
        }

        /// <summary>
        /// Change status of order with user rigths
        /// </summary>
        /// <param name="order">order to change status</param>
        private void ChangeOrderStatusByUser(OrderViewModel order)
        {
            bool isUpdated = false;
            while (true)
            {
                this.OutputOrder(order);
                if (order.State < OrderState.Received)
                {
                    Console.WriteLine("Received -to change status to Completed");
                    Console.WriteLine("CanceledByUser - to change status to CanceledByUser");
                }
                if (isUpdated)
                    Console.WriteLine("Update - to update status");
                Console.WriteLine("main - go to main page");
                Console.WriteLine("0 - Exit");

                this.userAction = this.GetUserText();

                if (userAction.Equals("Received") && order.State < OrderState.Received)
                {
                    order.State = OrderState.Received;
                    isUpdated = true;
                }
                else if (userAction.Equals("CanceledByUser") && order.State < OrderState.Received)
                {
                    order.State = OrderState.CanceledByUser;
                    isUpdated = true;
                }
                else if (userAction.Equals("Update") && isUpdated)
                {
                    this.Controller.OrderController.UpdateStatebyId(order.Id, order.State);
                    this.page = MainPage;
                    return;
                }

                if (userAction.Equals("main"))
                {
                    this.page = MainPage;
                    return;
                }
                else if (userAction.Equals("0") && this.User != null)
                {
                    this.page = SignOff;
                    return;
                }
            }

        }

        /// <summary>
        /// Create new order
        /// </summary>
        /// <param name="id">id of product that will be ordered</param>
        private void CreateNewOrder(string id)
        {
            var product = this.Controller.ProductController.GetProductByKey(id);
            this.OutputProduct(product);
            Console.WriteLine(@"confirm - confirm new order");
            Console.WriteLine("main - go to main page");
            Console.WriteLine("0 - Exit");
            OrderViewModel order = new OrderViewModel() { IdProduct = product.Id, Name = product.Name, EmailUser = this.User.Email, Price = product.Price };
            while (true)
            {
                this.OutputOrder(order);
                this.userAction = this.GetUserText();
                if (userAction.Equals("description"))
                {
                    Console.WriteLine("Description : ");
                    order.Description = this.GetUserText();
                }
                else if (userAction.Equals("confirm"))
                {
                    this.Controller.OrderController.AddOrder(order);
                    this.page = OrderHistory;
                    return;
                }
                else if (userAction.Equals("main"))
                {
                    this.page = MainPage;
                    return;
                }
                else if (userAction.Equals("0") && this.User != null)
                {
                    this.page = SignOff;
                    return;
                }
            }
        }


        /// <summary>
        /// Check is entered email strin is valid
        /// </summary>
        /// <param name="email">email to check</param>
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                static string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        /// <summary>
        /// Output common page information
        /// </summary>
        public void Header()
        {
            Console.WriteLine("  BEST ESHOP BY MAX  ");
            if (this.User != null)
                Console.WriteLine(" Autorized as {0}", this.User.UserName);
            else
                Console.WriteLine(" Autorized as Guest");
        }

        /// <summary>
        /// Output functions for admins
        /// </summary>
        private void WriteAdministratorMainFunctions()
        {
            Console.WriteLine("show users - watch personal information of users");
            Console.WriteLine("new product - add new products");

        }

        /// <summary>
        /// Output functions for registered users
        /// </summary>
        private void WriteRegisteredUserMainFunctions()
        {

            Console.WriteLine("show - list of products");
            Console.WriteLine("find - find a product by name");
            Console.WriteLine("watch history - order history");
            Console.WriteLine("persinf - personal information");
            Console.WriteLine("0 - exit");
        }

        /// <summary>
        /// Output functions for guests
        /// </summary>
        private void WriteGuestMainFunctions()
        {

            Console.WriteLine("show - list of products");
            Console.WriteLine("find - find a product by name");
            Console.WriteLine("sign up - registration ");
            Console.WriteLine("sign in - autorisation");
        }

        /// <summary>
        /// Output formated user information
        /// </summary>
        /// <param name="user">object with user information </param>
        private void OutputUser(UsersViewModel user)
        {
            if (user != null)
            {
                Console.WriteLine("Name : {0}", user.UserName);
                Console.WriteLine("Email : {0}", user.Email);
                Console.WriteLine("Password : {0} \n\n", user.Password);
            }
        }

        /// <summary>
        /// Output formated product information
        /// </summary>
        /// <param name="product">object with product information </param>
        private void OutputProduct(Product product)
        {
            if (product != null)
            {
                Console.WriteLine("Id : {0}", product.Id);
                Console.WriteLine("Name : {0}", product.Name);
                Console.WriteLine("Price : {0}", product.Price);
                Console.WriteLine("Description : {0} \n\n", product.Description);
            }
        }

        /// <summary>
        /// Output formated order information
        /// </summary>
        /// <param name="order">object with order information </param>
        private void OutputOrder(OrderViewModel order)
        {
            if (order != null)
            {
                Console.WriteLine("Id : {0}", order.Id);
                Console.WriteLine("Name : {0}", order.Name);
                Console.WriteLine("State : {0}", order.State);
                Console.WriteLine("Price : {0}", order.Price);
                Console.WriteLine("Description : {0} \n\n", order.Description);
            }
        }

        /// <summary>
        /// Clear console and invoke outputing common page information
        /// </summary>
        public void Clear()
        {
            Console.Clear();
            this.Header();
        }

        /// <summary>
        /// Return user action from console
        /// </summary>
        internal string GetUserText()
        {
            return Console.ReadLine().Trim();
        }
    }
}
