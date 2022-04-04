namespace Vending_machine
{
    public class Vending_Machine : Error_ID
    {
        Dictionary <int, Products_List> products_list = new Dictionary <int, Products_List> ();

        Dictionary <Double,int> available_coin = new Dictionary <Double,int> ();
        Dictionary<Double, int> coin_already_put = new Dictionary<Double, int>();

        language internalLangague = null;

        private double _intputAlreadyIn = 0;
        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Vending_Machine myVendingMachine = new Vending_Machine();
            myVendingMachine.initNewProductListAndCoins(args);
            myVendingMachine.startListenningUserCommand();
        }
        private void startListenningUserCommand()
        {
            string? user_response = null;
            do
            {
                Console.WriteLine("------------------\n"+
                internalLangague.mainMessage +
                "------------------\n" +
                internalLangague.command + " : ");
                user_response = Console.ReadLine();
            } while (user_response == null);
            switch (user_response.ToLower())
            {
                case "show":
                    showProductList();
                    break;
                case "return coins":
                    returnCoins();
                    break;
                default:
                    if (user_response.ToLower().StartsWith("select "))
                    {
                        selectFunction(user_response);
                    }
                    else if (user_response.ToLower().StartsWith("enter "))
                    {
                        enterCoinsFunction(user_response);
                    }
                    else if(user_response.ToLower().StartsWith("langague "))
                    {
                        changeLangague(user_response);
                    }
                    else
                    {
                        Console.WriteLine(internalLangague.invalidCommand);
                        startListenningUserCommand();
                    }
                    break;
            }
            
        }

        private void showProductList()
        {
            string finalStr = "";
            for(int i = 0; i < products_list.Count; i++)
            {
                if (products_list[i].getProductPrice() >= getTotalMonney())
                {
                    finalStr += "[" + i + "] " + products_list[i].getList(true) + "\n";
                }
                else
                {
                    finalStr += "[" + i + "] " + products_list[i].getList(false) + "\n";
                }
            }
            Console.WriteLine("------------------\n" +
                internalLangague.productList + " :\n" +
                finalStr +
                "------------------\n");
            startListenningUserCommand();
        }
        private void selectFunction(string p_request)
        {
            int requested_id = Int32.Parse(p_request.Split(' ')[1]);
            if (requested_id <= products_list.Count && requested_id > -1)
            {
                Console.WriteLine("------------------\n" +
                internalLangague.productAsk + " : " + products_list[requested_id].getName());
                if(products_list[requested_id].isAvailable() == SUCCESS)
                {
                    if(getHowMuchMoneyAlreadyPut() >= products_list[requested_id].getProductPrice()){
                        if (products_list[requested_id].getProductPrice() > getTotalMonney())
                        {
                            products_list[requested_id].purchaseOne();
                            addCoinsToTheMachine(products_list[requested_id].getProductPrice(), true);
                            Console.WriteLine(internalLangague.pruchaseSuccessfull);
                        }
                        else
                        {
                            products_list[requested_id].purchaseOne();
                            addCoinsToTheMachine(products_list[requested_id].getProductPrice(), false);
                            Console.WriteLine(internalLangague.pruchaseSuccessfull);
                        }
                    }
                    else
                    {
                        double price_left = Math.Round(products_list[requested_id].getProductPrice() - getHowMuchMoneyAlreadyPut(), 2);
                        Console.WriteLine(internalLangague.haveLeftToPay(price_left.ToString()));
                        startListenningUserCommand();
                    }
                }
                else
                {
                    Console.WriteLine(internalLangague.soldOutProduct + "\n");
                    startListenningUserCommand();
                }
            }
            else
            {
                Console.WriteLine("------------------\n" +
              internalLangague.errorOnSelectProduct + "\n" +
              "------------------\n");
                startListenningUserCommand();
            }
        }

        private void enterCoinsFunction(string p_request)
        {
            p_request = p_request.Replace('.', ',');
            double coins_entered = Double.Parse(p_request.Split(' ')[1]);
            if (isItAValidCoin(coins_entered))
            {
                coin_already_put[coins_entered] = coin_already_put[coins_entered] + 1;
                Console.WriteLine(internalLangague.amountEntered+ getHowMuchMoneyAlreadyPut());
                startListenningUserCommand();
            }
            else
            {
                Console.WriteLine(internalLangague.invalidcoin);
                startListenningUserCommand();
            }
        }

        private double getHowMuchMoneyAlreadyPut()
        {
            double finalValue = 0;
            foreach(KeyValuePair<Double,int> coins in coin_already_put)
            {
                finalValue = finalValue + coins.Value * coins.Key;
            }
            return finalValue;
        }

        private Boolean isItAValidCoin(double p_coin)
        {
            if (p_coin > 0) {
                switch (p_coin)
                {
                    case 0.05:
                    case 0.10:
                    case 0.20:
                    case 0.50:
                    case 1:
                    case 2:
                        return true;
                        //break;
                    default:
                        return false;
                        //break;
                }
            }
            else
            {
                return false;
            }
        }
        private void addCoinsToTheMachine(double p_price, bool exactChangeOnly)
        {
            if (exactChangeOnly)
            {
                for (int i = coin_already_put.Count - 1; i > 0; i--)
                {
                    available_coin[available_coin.ElementAt(i).Key] = (int)(available_coin[available_coin.ElementAt(i).Key] + coin_already_put[coin_already_put.ElementAt(i).Key]);
                }
            }
            else
            {
                double remaining = p_price;
                for (int i = coin_already_put.Count - 1; i > 0; i--)
                {
                    if (remaining <= 0)
                    {
                        break;
                    }

                    if (coin_already_put[coin_already_put.ElementAt(i).Key] > 0)
                    {
                        double priceNumber = coin_already_put.ElementAt(i).Key;
                        int quantity = coin_already_put.ElementAt(i).Value;
                        if (priceNumber <= remaining)
                        {
                            double resultEntier = Math.Floor(remaining / priceNumber);
                            if (resultEntier > 0)
                            {
                                double remainToAdd = remaining / priceNumber - Math.Truncate(remaining / priceNumber);
                                if (quantity >= resultEntier)
                                {
                                    coin_already_put[coin_already_put.ElementAt(i).Key] = (int)(coin_already_put[coin_already_put.ElementAt(i).Key] - resultEntier); //the given key is not present in the dictionary
                                    available_coin[available_coin.ElementAt(i).Key] = (int)(available_coin[available_coin.ElementAt(i).Key] + resultEntier);
                                    //remaining = remaining - resultEntier * priceNumber;
                                    remaining = (remaining - resultEntier * priceNumber) - ((quantity - resultEntier) * priceNumber);
                                }
                                else
                                {
                                    coin_already_put[coin_already_put.ElementAt(i).Key] = 0;
                                    available_coin[available_coin.ElementAt(i).Key] = (int)(available_coin[available_coin.ElementAt(i).Key] + quantity);
                                    remaining = remaining - resultEntier * priceNumber;
                                }
                            }
                        }
                        else
                        {
                            remaining = remaining - priceNumber;
                            available_coin[available_coin.ElementAt(i).Key] = (int)(available_coin[available_coin.ElementAt(i).Key] + 1);
                            coin_already_put[coin_already_put.ElementAt(i).Key] = 0;
                        }
                    }
                }
                if (getHowMuchMoneyAlreadyPut() > 0 && remaining >= 0 || getHowMuchMoneyAlreadyPut() == Math.Abs(remaining))
                {
                    returnCoins();
                }
                else if (remaining < 0)
                {
                    hasToReturnCoins(remaining);
                }
                else
                {
                    Console.WriteLine(internalLangague.thankYou);
                }
            }
        }
        private void hasToReturnCoins(double remaining)
        {
            double moneyToReturn = Math.Abs(remaining);
            for (int i = available_coin.Count - 1; i > 0; i--)
            {
                if(moneyToReturn <= 0)
                {
                    break;
                }
                if (available_coin[available_coin.ElementAt(i).Key] > 0)
                {
                    double priceNumber = available_coin.ElementAt(i).Key;
                    int quantity = available_coin.ElementAt(i).Value;
                    if (priceNumber <= moneyToReturn)
                    {
                        double resultEntier = Math.Floor(moneyToReturn / priceNumber);
                        if (resultEntier > 0)
                        {
                            double remainToAdd = moneyToReturn / priceNumber - Math.Truncate(moneyToReturn / priceNumber);
                            if (quantity >= resultEntier)
                            {
                                available_coin[available_coin.ElementAt(i).Key] = (int)(available_coin[available_coin.ElementAt(i).Key] - resultEntier); //the given key is not present in the dictionary
                                coin_already_put[available_coin.ElementAt(i).Key] = (int)(coin_already_put[available_coin.ElementAt(i).Key] + resultEntier);
                                moneyToReturn = moneyToReturn - resultEntier * priceNumber;
                            }
                            else
                            {
                                available_coin[available_coin.ElementAt(i).Key] = 0;
                                coin_already_put[coin_already_put.ElementAt(i).Key] = (int)(coin_already_put[coin_already_put.ElementAt(i).Key] + quantity);
                                moneyToReturn = moneyToReturn - resultEntier * priceNumber;
                            }
                        }
                    }
                }
                
            }
            returnCoins();
        }
        private void returnCoins()
        {
            double finalValue = getHowMuchMoneyAlreadyPut();
            if (finalValue > 0)
            {
                foreach (KeyValuePair<Double, int> coins in coin_already_put)
                {
                    if (coins.Value > 0)
                    {
                        Console.WriteLine(internalLangague.nbMoneyReturn(coins.Value.ToString(), coins.Key.ToString()));
                    }
                }
                Console.WriteLine(finalValue + internalLangague.euroreturn);
                for (int i = coin_already_put.Count - 1; i > 0; i--)
                {
                    if (coin_already_put[coin_already_put.ElementAt(i).Key] > 0)
                    {
                        coin_already_put[coin_already_put.ElementAt(i).Key] = 0;
                    }
                }
                startListenningUserCommand();
            }
            else
            {
                Console.WriteLine(internalLangague.noCoinPut);
                startListenningUserCommand();
            }
        }
        private double getTotalMonney()
        {
            double finalValue = 0;
            foreach (KeyValuePair<Double, int> coins in available_coin)
            {
                finalValue = finalValue + coins.Value * coins.Key;
            }
            return finalValue;
        }
        private void changeLangague(string langagueCommand)
        {
            string langagueSelect = langagueCommand.Split(" ")[1];
            internalLangague = new language(langagueSelect.ToLower());
            for (int i = 0; i < products_list.Count; i++)
            {
                products_list[i].setLangague(internalLangague);
                if (products_list[i].getProductPrice() >= getTotalMonney()) ;
            }
            startListenningUserCommand();
        }
        private void initNewProductListAndCoins(string[] args)
        {
            available_coin.Add(0.05, 10);
            available_coin.Add(0.10, 10);
            available_coin.Add(0.20, 10);
            available_coin.Add(0.50, 10);
            available_coin.Add(1, 10);
            available_coin.Add(2, 10);

            /*
            available_coin.Add(0.05, 0);
            available_coin.Add(0.10,0);
            available_coin.Add(0.20, 0);
            available_coin.Add(0.50, 0);
            available_coin.Add(1, 0);
            available_coin.Add(2, 0);
            */

            coin_already_put.Add(0.05, 0);
            coin_already_put.Add(0.10, 0);
            coin_already_put.Add(0.20, 0);
            coin_already_put.Add(0.50, 0);
            coin_already_put.Add(1, 0);
            coin_already_put.Add(2, 0);
            products_list.Add(0, new Products_List(0, "cola", 1, internalLangague, 15));
            products_list.Add(1, new Products_List(1, "chips", 0.5, internalLangague, 10));
            products_list.Add(2, new Products_List(2, "candy", 0.65, internalLangague, 20));

            internalLangague = new language();
            if (args.Length == 0)
            {
                
            }
            else
            {
                for (int i = 0; i < args.Length; i++)
                {
                    string argument = args[i];                    
                }
            }
        }
    }
}