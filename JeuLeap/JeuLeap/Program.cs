using System;
using System.Threading;
using System.Net.Http;
using Leap;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace JeuLeap
{
    class SampleListener : Listener
    {
        private Object thisLock = new Object();
        private String url = "http://esaip.westeurope.cloudapp.azure.com/";

        /// <summary>
        /// on affiche le fichier json récupérer dans l'url
        /// </summary>
        public void initSampleListener()
        {
            SafeWriteLine("init...");
            SafeWriteLine(GetDemandes());
        }
        private void SafeWriteLine(String line)
        {
            lock (thisLock)
            {
                Console.WriteLine(line);
            }
        }

        public override void OnInit(Controller controller)
        {
            SafeWriteLine("Initialized\n");

        }

        public override void OnConnect(Controller controller)
        {

            controller.EnableGesture(Gesture.GestureType.TYPE_CIRCLE);
            controller.EnableGesture(Gesture.GestureType.TYPE_KEY_TAP);
            controller.EnableGesture(Gesture.GestureType.TYPE_SCREEN_TAP);
            controller.EnableGesture(Gesture.GestureType.TYPE_SWIPE);
        }

        public override void OnDisconnect(Controller controller)
        {
            SafeWriteLine("Disconnected\n");
        }

        public override void OnExit(Controller controller)
        {
            SafeWriteLine("Exited\n");
        }

        

        /// <summary>
        /// Evènement lorsque le capteur de la leap détecte une main. 
        /// Puis selon la position de la main(centre de la paume), nous détectons la réponse fournie par l'utilisation
        /// </summary>
        /// <param name="Controller">L'objet controller pour la leap</param>
        
        public override void OnFrame(Controller controller)
        {
            Frame frame = controller.Frame();

            Hand hand = frame.Hands.Rightmost;
            Vector position = hand.PalmPosition;
            float timeVisible = hand.TimeVisible;
            if (frame.Hands.Count > 0)
            {
                Hand h = frame.Hands[0];
                SafeWriteLine("taux de confiance=" + h.Confidence); //affiche le taux de confiance de détection de la leapmotion 0<x<1
                if (h.Confidence >= 0.8)
                {
                    if (frame.Hands.Count > 1 || frame.Fingers.Count > 5)
                    {
                        SafeWriteLine("Vous devez voter qu'avec 1 seule main."); //lorsque la LeapMotion intercepete 2 mains en meme temps
                    }                                                            //on précise à l'utilisateur que le vote se fait à une main
                    else
                    {
                        if (h.IsLeft == true)
                        {
                            SafeWriteLine("Vous avez voté NON."); //réponse négative pour le vote
                        }
                        if (h.IsRight == true)
                        {
                            SafeWriteLine("Vous avez voté OUI."); // réponse positive pour le vote
                        }
                    }
                }
                else
                {
                    SafeWriteLine("recommencer SVP"); //lorsque le taux de confiance est <0.8 on demande à l'utilisateur de recommencer son vote
                }
                System.Threading.Thread.Sleep(1000);
            }
        }

        /*************************************/
        /*Méthode pour récuperer les demandes*/
        /*************************************/
        public string GetDemandes()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url+"api/Requests");
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }

        }
        


        /***********************************/

        class Sample
        {
            public static void Main()
            {
                SampleListener listener = new SampleListener();
                Controller controller = new Controller();
                controller.AddListener(listener);
                Console.WriteLine("Appuyez sur la touche échape pour quitter: \n");
                listener.initSampleListener();
                while (true) ;

            }
        }
    }
}