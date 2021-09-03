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
                Console.WriteLine("[4] Admin");
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

                    case "3":
                        AddAReview();
                    break;

                    //case "4":
                    //    ViewAllCustomers();
                    //break;
                    case "4":
                        Console.WriteLine("Please enter password");
                        switch(Console.ReadLine())
                        {
                            case "40":
                                Console.WriteLine("Welcome, to search users please enter one of the clue which could be name or username");
                                searchUsersByName();
                                break;
                            default:
                                Console.WriteLine("your password is wrong! :(");
                                break;
                        }
                    break;

                    case "43":
                        searchUsersByName();//admin
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
            Console.WriteLine("[1] Search restaurant by name\n[2] Search restaurant by zipcode");
            string input;
            //find a restaurant by 2 options
            switch(Console.ReadLine())
            {   case "1":
            
            Console.WriteLine("Enter the name of the restaurant to search: ");
            input = Console.ReadLine();
            if(input == '') {
                Console.Writeline("You wrote an empty search")
            }
            Restaurant foundRestaurant = _restaurantbl.FindARestaurant(input);
            if(foundRestaurant.Name is null)
            {
                Console.WriteLine($"{input} is might not on our list");
            }
            else {
                Console.WriteLine($"{foundRestaurant.Name}\nAddress: {foundRestaurant.Location}\nContact: {foundRestaurant.Contact}");
                Console.WriteLine("---------------------------");
                DisplayAvgRating(foundRestaurant);
            }
                
                break;

            case "2":
                string input1;
                Console.WriteLine("Enter Zipcode of the restaurant to search: ");
                input1 = Console.ReadLine();
                List<Restaurant> restaurants = _restaurantbl.ViewAllRestaurants();
                for (int i = 0; i<restaurants.Count; i++)
                {
                    if(restaurants[i].Zipcode == input1)
                    Console.WriteLine($"[{i}]{restaurants[i].Name} | {restaurants[i].Location} | Phone number: {restaurants[i].Contact}");
                    Console.WriteLine("---------------------------");
                }
                break;
            
            default:
                Console.WriteLine("The restaurant you are trying to search is not in our list. Sorry!");
                break;
            }
        
        }
        
        //try to seperate the rating average
        private DisplayAvgRating(Restaurant foundRestaurant)
        {
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
                Console.WriteLine(foundCustomer.FirstName + " " + foundCustomer.LastName + " | " + foundCustomer.UserName + " | " + foundCustomer.Email);
                Console.WriteLine("------------------------------------");
                Console.WriteLine("Reviews this user put on restaurants");
                List<ReviewJoin> reviewjoins = _reviewjoinbl.ViewReviewJoins();
                for(int i = 0; i < reviewjoins.Count; i++)
                {
                    if(reviewjoins[i].CustomerId==foundCustomer.Id)
                    {
                        Review foundReview = _reviewbl.SearchReviewByReviewId(reviewjoins[i].ReviewId);
                        Restaurant foundRestaurant = _restaurantbl.FindARestaurantById(reviewjoins[i].RestaurantId);
                        Console.WriteLine($"{foundRestaurant.Name} | {foundReview.Comment}");
                    }
                }
            }
        }

        /// <summary>
        /// get input1 for user's lastname, input2 for restaurant name, input 3 for review, and input 4 for rating
        /// Add to Review Table
        /// get the CustomerId, RestaurantId, ReviewId and add to ReviewJoin table
        /// </summary>
        private void AddAReview()
        {
            Console.WriteLine("To add reviews, you must be a member with us. Do you want to keep adding?");
            Console.WriteLine("[1] Keep adding \n[2] Sign-up to be a member\n[3] Exit");
            switch(Console.ReadLine()){
                case "1":
                string input1;
                string input2;
                string input3;
                string input4;

                List<Customer> customers = _customerbl.ViewAllCustomers();
                List<Review> reviews = _reviewbl.ViewReview();
                List<ReviewJoin> reviewjoins = _reviewjoinbl.ViewReviewJoins();

                
                do{Console.WriteLine("Please enter your last name");
                input1 = Console.ReadLine();}while(String.IsNullOrWhiteSpace(input1));
                Customer foundCustomer = _customerbl.searchUsersByName(input1); //foundCustomer.Id;
                if (foundCustomer.FirstName is null) Console.WriteLine("You are not a member with us :(");

                else{
                do{Console.WriteLine("Please enter restarant to add review");
                input2 = Console.ReadLine();}while(String.IsNullOrWhiteSpace(input2));
                Restaurant foundRestaurant = _restaurantbl.FindARestaurant(input2);//foundRestaurant.Id
                if (foundRestaurant.Name is null) Console.WriteLine("The restaurant you choose is not in our list :(");

                
                else{
                do{Console.WriteLine("Please add your review");
                input3 = Console.ReadLine();}while(String.IsNullOrWhiteSpace(input3));

                Console.WriteLine("Please add your rating from 1 to 5");
                  
                input4 = Console.ReadLine();
                while ((Convert.ToDecimal(input4)>5 || Convert.ToDecimal(input4)<=0))
                {Console.WriteLine("You rating is out of range. Please enter again");
                input4 = Console.ReadLine();}
                

                int c = reviews[reviews.Count-1].Id; //reviewId
                Review reviewToAdd = new Review(c+1, input3, input4, DateTime.Now);
                reviewToAdd = _reviewbl.AddReview(reviewToAdd);
                
                ReviewJoin reviewjoinToAdd = new ReviewJoin(foundRestaurant.Id, foundCustomer.Id, c+1);
                reviewjoinToAdd = _reviewjoinbl.AddAReviewJoin(reviewjoinToAdd);
                Console.WriteLine("your review had been added.");}}
                    break;
                case "2":
                    AddAUser();
                    break;
                case "3":
                    Console.WriteLine("Thank you for your visiting!");
                    break;
        
            }
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
