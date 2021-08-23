using Models;
using BL;

namespace UI
{
    public class MainMenu:IMenu
    {
        private ICustomerBL _customerbl;
        private IRestaurantBL _restaurantbl;
        private IReviewBL _reviewbl;
        private IReviewJoinBL _reviewjoinbl;

        public MainMenu(ICustomerBL bl, IRestaurantBL rbl, IReviewBL rebl, IReviewJoinBL rejbl)
        {
            _customerbl = bl;
            _restaurantbl = rbl;
            _reviewbl = rebl;
            _reviewjoinbl = rejbl;
        }
        
        public void Start()
        {
            bool repeat = true;
            do
            {
                Console.WriteLine("Welcome to Restaurant Review Project!");
                Console.WriteLine("[0] Exit");
                Console.WriteLine("[1] Add a User");
                Console.WriteLine("[2] Find Restaurants");
                Console.WriteLine("[3] Add a Review");
                //Console.WriteLine("[43] Search User");

                switch(Console.ReadLine())
                {
                    case "0":
                        Console.WriteLine("Goodbye!");
                        repeat = false;
                    break;

                    case "1":
                        AddAUser();
                    break;

                    case "2":
                        FindARestaurant();
                    break;

                    case "43":
                        searchUsersByName();//admin
                    break;

                    case "3":
                        AddAReview();
                    break;

                    case "4":
                        ViewAllCustomers();
                    break;

                    default:
                        Console.WriteLine("Please select number to go to where you want to go to or press 0 to exit");
                    break;
                }
            } while(repeat);
        }

        private void AddAUser()
        {
            string input1;
            string input2;
            string input3;
            string input4;
            Customer customerToAdd;
            Console.WriteLine("Enter your information to add");

            do
            {
                Console.WriteLine("Name: ");
                input1=Console.ReadLine();
                Console.WriteLine("LastName:");
                input2=Console.ReadLine();
                Console.WriteLine("username:");
                input3=Console.ReadLine();
                Console.WriteLine("Email:");
                input4=Console.ReadLine();
            } while ((String.IsNullOrWhiteSpace(input1)) && (String.IsNullOrWhiteSpace(input2)) && (String.IsNullOrWhiteSpace(input3)) && (String.IsNullOrWhiteSpace(input4)));

            customerToAdd = new Customer(input1, input2, input3, input4);
            customerToAdd = _customerbl.AddAUser(customerToAdd);
            Console.WriteLine("Your account has been added");
        }

        /// <summary>
        /// get input for restaurant Name
        /// use reviewjoin table to find reviews matching the restaurant
        /// find reviews by foundrestaurant.Id
        /// </summary>
        private void FindARestaurant()
        {
            string input;
            Console.WriteLine("Enter the name of the restaurant to search: ");
            input = Console.ReadLine();

            Restaurant foundRestaurant = _restaurantbl.FindARestaurant(input);
            if(foundRestaurant.Name is null)
            {
                Console.WriteLine($"{input} is might not on our list");
            }
            else {
                Console.WriteLine($"{foundRestaurant.Name}\nAddress: {foundRestaurant.Location}\nContact: {foundRestaurant.Contact}");
                Console.WriteLine("---------------------------");

                decimal sum = 0;
                int n = 0;
                decimal average = 1;
                List<ReviewJoin> reviewjoins = _reviewjoinbl.ViewReviewJoins();

                Console.WriteLine("REVIEWS");
                for(int i = 0; i < reviewjoins.Count; i++)
                {
                    if(reviewjoins[i].RestaurantId==foundRestaurant.Id)
                    {
                    //Console.WriteLine(reviewjoins[i].ReviewId);
                    Review foundReview = _reviewbl.SearchReviewByReviewId(reviewjoins[i].ReviewId);
                    Console.WriteLine($"{foundReview.Comment} {foundReview.Time}");
                    sum += Convert.ToDecimal(foundReview.Rating);
                    n += 1;    
                    }
                }
                
                try
                {
                    average = (sum/n);
                }
                catch(DivideByZeroException)
                {
                    Console.WriteLine("Division of {0} by zero.", sum);
                }
                
                decimal average1 = Math.Round(average,2);
                Console.WriteLine("---------------------------");
                Console.WriteLine($"Average Rating: {average1}");
            }
                Console.WriteLine("---------------------------");
        }

        private void searchUsersByName()
        {
            string input;
            Console.WriteLine("Enter customer name");
            input = Console.ReadLine();

            Customer foundCustomer = _customerbl.searchUsersByName(input);
            if(foundCustomer.LastName is null)
            {
                Console.WriteLine($"Noone with {input} in the list");
            }
            else{
                Console.WriteLine(foundCustomer.FirstName + " " + foundCustomer.LastName + " " + foundCustomer.UserName + " " + foundCustomer.Email);
            }
        }

        /// <summary>
        /// get input1 for user's lastname, input2 for restaurant name, input 3 for review, and input 4 for rating
        /// Add to Review Table
        /// get the CustomerId, RestaurantId, ReviewId and add to ReviewJoin table
        /// </summary>
        private void AddAReview()
        {
            List<Customer> customers = _customerbl.ViewAllCustomers();
            List<Review> reviews = _reviewbl.ViewReview();
            List<ReviewJoin> reviewjoins = _reviewjoinbl.ViewReviewJoins();
  
            Console.WriteLine("Please enter your last name");
            string input1 = Console.ReadLine();
            Customer foundCustomer = _customerbl.searchUsersByName(input1); //foundCustomer.Id;

            Console.WriteLine("Please enter restarant to add review");
            string input2 = Console.ReadLine();
            Restaurant foundRestaurant = _restaurantbl.FindARestaurant(input2);//foundRestaurant.Id

            Console.WriteLine("Please add your review");
            string input3 = Console.ReadLine();
            Console.WriteLine("Please add your rating");
            string input4 = Console.ReadLine();
  
                int c = reviews[reviews.Count-1].Id; //reviewId
                Review reviewToAdd = new Review(c+1, input3, input4);
                reviewToAdd = _reviewbl.AddReview(reviewToAdd);
                
                ReviewJoin reviewjoinToAdd = new ReviewJoin(foundRestaurant.Id, foundCustomer.Id, c+1);
                reviewjoinToAdd = _reviewjoinbl.AddAReviewJoin(reviewjoinToAdd);
                Console.WriteLine("your review had been added.");                 
        }

        private void ViewAllCustomers()
        {
            List<Customer> customers = _customerbl.ViewAllCustomers();
            foreach (Customer customer in customers)
            {
                Console.WriteLine(customer.FirstName + customer.LastName + " " + customer.UserName);
            }
        }
    }
}