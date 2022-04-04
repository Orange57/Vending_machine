using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending_machine
{
    internal class language
    {
        public string thankYou { get; set; }
        public string invalidcoin { get; set; }
        public string errorOnSelectProduct { get; set; }
        public string soldOutProduct { get; set; }
        public string pruchaseSuccessfull { get; set; }
        public string noCoinPut { get; set; }
        public string productList{ get; set; }
        public string invalidCommand{ get; set; }
        public string productAsk { get; set; }
        public string command { get; set; }
        public string mainMessage { get; set; }
        public string amountEntered { get; set; }
        public string euroreturn { get; set; }
        public string moneyReturn { get; set; }

        public string nbMoneyReturn(string coinValue, string coinKey)
        {
            string toReturn = moneyReturn.Replace("%0", coinValue);
            toReturn = toReturn.Replace("%1", coinKey);
            return toReturn;
        }

        public string productPrice { get; set; }

        public string haveLeftToPayText { get; set; }
        public string haveLeftToPay(string priceleft)
        {
            return haveLeftToPayText.Replace("%0", priceleft);
        }
        public string idcannotbeempty { get; set; }
        public string namecannotbeempty { get; set; }
        public string pricecannotbeempty { get; set; }
        public string itemLeftExactOnly { get; set; } 
        public string itemLeft { get; set; } 
        public string soldout { get; set; } 
        public language()
        {
            initLanguage("en");
        }
        public language(string lang)
        {
            initLanguage(lang);
        }

        private void initLanguage(string lang)
        {
            switch (lang)
            {
                case "fr":
                    thankYou = "MERCI BEAUCOUP";
                    invalidcoin = "Piece invalide, merci de réessayer.";
                    errorOnSelectProduct = "ERREUR LORS DE LA SELECTION DE PRODUIT";
                    soldOutProduct = "Desolé le produit est actuellement indisponible, merci de ressayer plus tard...";
                    pruchaseSuccessfull = "Achat effectue avec succes, merci.";
                    noCoinPut = "Aucune piece inserer";
                    productList = "LISTES DES PRODUITS";
                    invalidCommand = "COMMANDE INVALIDE, merci de reesayer";
                    productAsk = "PRODUIT DEMANDE";
                    command = "COMMANDE";
                    mainMessage = "Bienvenue sur le distributeur, comment puis-je vous aider ?\n" +
                    "Pour changer de langue : Saisir 'LANGAGUE <EN|DE|FR>'\n" +
                    "Voulez vous acceder a la liste des produits ? Saisir 'SHOW'\n" +
                    "Voulez vous selectionner un produit? Saisir 'SELECT <ID DU PRODUIT>'\n" +
                    "Voulez vous inserrer des pieces ? Saisir 'ENTER <XXX>'\n" +
                    "Peut etre voulez vous recuperer vos pieces ? Saisir 'RETURN COINS'\n";
                    amountEntered = "Somme saisie : ";
                    euroreturn = "€ rendu";
                    moneyReturn = "Rend %0 piece de %1€";
                    productPrice = "prix du produit : ";
                    haveLeftToPayText = "Il vous reste %0€ à payer";
                    idcannotbeempty = "l'ID ne peut pas etre vide";
                    namecannotbeempty = "Le nom ne peut pas etre vide";
                    pricecannotbeempty = "Le prix ne peut pas etre vide";
                    itemLeftExactOnly = " restant. MONNAIE EXACTE SEULEMENT";
                    itemLeft = " restant";
                    soldout = "EPUISE";
                    break;
                case "de":
                    thankYou = "DANKE";
                    invalidcoin = "ungültige Münze eingeworfen, bitte versuchen Sie es erneut.";
                    errorOnSelectProduct = "FEHLER BEI PRODUKTAUSWAHL";
                    soldOutProduct = "Entschuldigung, das Produkt ist tatsächlich ausverkauft, versuchen Sie es später erneut...";
                    pruchaseSuccessfull = "Kauf erfolgreich, danke.";
                    noCoinPut = "Keine Münzen bereits gesetzt";
                    productList = "PRODUKTLISTE";
                    invalidCommand = "UNGÜLTIGER BEFEHL, bitte versuchen Sie es erneut";
                    productAsk = "PRODUKT FRAGEN";
                    command = "BEFEHL";
                    mainMessage = "Willkommen beim Verkaufsautomaten, wie kann ich Ihnen heute helfen ?\n" +
                    "Zum Ändern der Sprache: Eingeben 'LANGAGUE <EN|DE|FR>'\n" +
                    "Wollten Sie die Produktliste erhalten? Eingeben 'SHOW'\n" +
                    "Wollten Sie ein Produkt auswählen ? Eingeben 'SELECT <ID OF PRODUCT>'\n" +
                    "Wollten Sie Münzen einwerfen ? Eingeben 'ENTER <XXX>'\n" +
                    "Vielleicht möchten Sie Ihr Geld zurückgeben ? Eingeben 'RETURN COINS'\n";
                    amountEntered = "Betrag eingegeben : ";
                    euroreturn = "€ Rückkehr";
                    moneyReturn = "Rückkehr %0 münzen von %1€";
                    productPrice = "produktpreis : ";
                    haveLeftToPayText = "Du hast %0€ noch zu zahlen";
                    idcannotbeempty = "ID darf nicht leer sein";
                    namecannotbeempty = "Der Name darf nicht leer sein";
                    pricecannotbeempty = "Der Preis darf nicht leer sein";
                    itemLeftExactOnly = "Artikel übrig. NUR GENAUE ÄNDERUNG";
                    itemLeft = "Artikel übrig";
                    soldout = "AUSVERKAUFT";
                    break;
                case "en":
                default:
                    thankYou = "THANK YOU";
                    invalidcoin = "invalid coin inserted, please try again.";
                    errorOnSelectProduct = "ERROR ON SELECT PRODUCT";
                    soldOutProduct = "Sorry product is actually sold out, try again later...";
                    pruchaseSuccessfull = "Purchase successful, thank you.";
                    noCoinPut = "No coins already put";
                    productList = "PRODUCT LIST";
                    invalidCommand = "INVALID COMMAND, please try again";
                    productAsk = "PRODUCT ASK";
                    command = "COMMAND";
                    mainMessage = "Welcome to the Vending Machine, how can I help you today ?\n" +
                    "For changing langague : Type 'LANGAGUE <EN|DE|FR>'\n" +
                    "Did you want to get the product list ? Type 'SHOW'\n" +
                    "Did you want to select a product ? Type 'SELECT <ID OF PRODUCT>'\n" +
                    "Did you want to insert coins ? Type 'ENTER <XXX>'\n" +
                    "Maybe you want to return your money ? Type 'RETURN COINS'\n";
                    amountEntered = "Amount entered : ";
                    euroreturn = "€ return";
                    moneyReturn = "Return %0 coins of %1€";
                    productPrice = "produt price : ";
                    haveLeftToPayText = "You have %0€ left to pay";
                    idcannotbeempty = "ID can't be empty";
                    namecannotbeempty = "Name can't be empty";
                    pricecannotbeempty = "Price can't be empty";
                    itemLeftExactOnly ="item left. EXACT CHANGE ONLY";
                    itemLeft = "item left";
                    soldout = "SOLD OUT";
                    break;
            }
        }
    }
}
