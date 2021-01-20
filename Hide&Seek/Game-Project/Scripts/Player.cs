using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace NocVedcu
{
    public class Player : MonoBehaviour
    {

        //playeMovement
        public PlayerMovementController playerMovement = null;
        public PauseMenu pauseMenu=null;
        public Text napoveda;
        public GameObject Cat;
        public Text Quests;
        public GameObject finish;
        public Text finishTime;
        public GameObject timeCounter;

        //Instrukce
        public GameObject DoorInstruction;
        public GameObject TakeInstruction;
        public GameObject SubmitInstruction;
        public GameObject LockDoorInstruction;
        public GameObject LayInstruction;

        //Najdi
        public GameObject Kolecka;
        public GameObject Antena;
        public GameObject Nabijecka;
        public GameObject Key;
        public GameObject Dezinfekce;
        public GameObject Robot;
        public GameObject Pepper;
        public GameObject LockedDoor;
        public GameObject Stul;
        public GameObject DezinfekceNaStole;

        private bool doorKey;
        private bool tryDoor=false;
        public static bool gameDone=false;

        private bool[] quests= {false,false,false,false,false,false,false,false,false,false,false};

        private string[] napovedy = {
            "1. Najdi náhradní kolečka na naší robotickou kočičku. Náhradní kolečka by měla být v přednášecí místnosti TK-3. Nechal jsem je na poličce v zadní časti posluchárny.",
            "2. Najdi novou anténku. Naše robotická kočka potřebuje příjmat signál. Nová anténa by měla být v počítačové třídě A-3, někde v zadní části třídy na zemi pod oknem. Tato třída se nachází na konci hlavní chodby po pravé straně",
            "3. Najdi nápajecí kabel. Naše robotická kočka potřebuje šťávu. Měl by být ve třídě AP-9, třída vedle šatny. Hledej v zadní časti místnosti",
            "4. Dones náhradní součástky Pepper, aby mohla dokončit opravu kočky. Pepper se nachází v robotické laboratoři.",
            "5. Dojdi pro bílého robota, tento robot by se měl nacházet na Sekretariátu naproti posluchárně TK-3.",
            "6. Tyto dveře jsou zamčené. Musíš najít klíč. Projdi šatnu, je možné že ho tam někdo zapomněl.",
            "7. Odemkni dveře a najdi robota, který netrpělivě přešlapuje za zamknutými dveřmi.",
            "8. Přines robota Pepper do robotické Laboratoře.",
            "9. Najdi láhev s dezinfekcí, bude potřeba až se znovu otevře škola. Dezinfekci skladujeme v technické místnosti vedle dívčích záchodů.",
            "10. Umísti dezinfekci na stůl ke vchodovým dveřím, bude tam potřeba až se vrátí učitelé a žáci do školy.",
            "Výborně, splnil jsi všechny úkoly. Gratulujeme."
        };
        /*
        private string[] Chvala = {
            "Ahoj, já jsem Pepper, já jsem zadavatel úkolů, tato hra se jmenuje Hide and Seek, nebo-li Hra na schovávanou. Toto robotickí kočka jménem Tom, bude tě navigovat.",
            "Jejda to je, ale nadělení. Náše robotická kočka se rozbila. Musíme to opravit, potřeboval bych tvojí pomoc při hledání nových součástek",
            "Výborně, donesl jsi potřebné součástky a mohu kočku tedy opravit, ale bude to ještě trvat, zatím mi dones další potřebné předměty.",
            "Blahopřejeme, splnil jsi všechny úkoly."
        };
        */
        private void Start()
        {
            Kolecka.SetActive(true);
            Antena.SetActive(false);
            Nabijecka.SetActive(false);
            Key.SetActive(false);
            Dezinfekce.SetActive(false);
            doorKey = false;
            Robot.SetActive(false);
        }
        void OnCollisionEnter(Collision collision)
        {
            //Output the Collider's GameObject's name
            Debug.Log(collision.collider.name);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Door")) {
                DoorInstruction.SetActive(true);
                Animator anim = other.GetComponentInChildren<Animator>();
                if (Input.GetKeyDown(KeyCode.E)) {
                    anim.SetTrigger("CloseOpen");
                }
            }
            //Kolecka - 1 ukol
            if (other.gameObject.CompareTag("Kolecka"))
            {
                TakeInstruction.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    quests[0] = true;
                    Antena.SetActive(true);
                    Kolecka.SetActive(false);
                    TakeInstruction.SetActive(false);
                    Quests.text = "1/10";
                }
            }
            //Antena - 2 ukol
            if (other.gameObject.CompareTag("Antena"))
            {
                TakeInstruction.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    quests[1] = true;
                    Nabijecka.SetActive(true);
                    Antena.SetActive(false);
                    TakeInstruction.SetActive(false);
                    Quests.text = "2/10";
                }
            }
            //Nabijecka - 3 ukol
            if (other.gameObject.CompareTag("Nabijecka")) {
                TakeInstruction.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    quests[2] = true;
                    Nabijecka.SetActive(false);
                    TakeInstruction.SetActive(false);
                    Quests.text = "3/10";
                    Pepper.GetComponent<BoxCollider>().enabled = true;
                }
            }
            //Odevzdej - 4 ukol
            if (other.gameObject.CompareTag("Pepper"))
            {
                if (Input.GetKeyDown(KeyCode.F) && doorKey == false)
                {
                    quests[3] = true;
                    SubmitInstruction.SetActive(false);
                    Quests.text = "4/10";
                    Pepper.GetComponent<BoxCollider>().enabled = false;
                    LockedDoor.tag = "LockDoor";
                    Animator anim = LockedDoor.GetComponent<Animator>();
                    if (anim.GetCurrentAnimatorStateInfo(0).IsName("Door2_Open")) { anim.SetTrigger("CloseOpen"); }
                    Pepper.GetComponent<Animator>().SetTrigger("Dones");
                }
            }
            //ZamceneDvere - 5 ukol
            if (other.gameObject.CompareTag("LockDoor"))
            {
                if (Input.GetKeyDown(KeyCode.E) && doorKey == false)
                {
                    tryDoor = true;
                    quests[4] = true;
                    Key.SetActive(true);
                    DoorInstruction.SetActive(false);
                    LockDoorInstruction.SetActive(true);
                    Quests.text = "5/10";
                }
            }
            //Klic - 6 ukol
            if (other.gameObject.CompareTag("Key"))
            {
                TakeInstruction.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    doorKey = true;
                    quests[5] = true;
                    Key.SetActive(false);
                    TakeInstruction.SetActive(false);
                    LockedDoor.tag = "Door";
                    Pepper.GetComponent<BoxCollider>().enabled = true;
                    Quests.text = "6/10";
                    Robot.SetActive(true);
                }
            }
            //LockDoor - 7 ukol
            if (other.gameObject.CompareTag("Robot"))
            {
                if (Robot.activeSelf) {
                    TakeInstruction.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.F) && doorKey == true)
                    {
                        quests[6] = true;
                        Robot.SetActive(false);
                        TakeInstruction.SetActive(false);
                        Pepper.GetComponent<BoxCollider>().enabled = true;
                        Quests.text = "7/10";
                    }
                }
            }
            //Pepper - 8 ukol
            if (other.gameObject.CompareTag("Pepper"))
            {
                if (Input.GetKeyDown(KeyCode.F) && doorKey == true)
                {
                    quests[7] = true;
                    SubmitInstruction.SetActive(false);
                    Dezinfekce.SetActive(true);
                    Quests.text = "8/10";
                    Pepper.GetComponent<BoxCollider>().enabled = false;
                    Pepper.GetComponent<Animator>().SetTrigger("Dones");
                }
            }
            //Dezinfekce - 9 ukol
            if (other.gameObject.CompareTag("Dezinfekce"))
            {
                TakeInstruction.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    quests[8] = true;
                    Dezinfekce.SetActive(false);
                    TakeInstruction.SetActive(false);
                    Stul.GetComponent<BoxCollider>().enabled = true;
                    Quests.text = "9/10";
                }
            }
            //Stul - 10 ukol
            if (other.gameObject.CompareTag("Stul"))
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    quests[9] = true;
                    LayInstruction.SetActive(false);
                    DezinfekceNaStole.SetActive(true);
                    Stul.GetComponent<BoxCollider>().enabled = false;
                    Quests.text = "10/10";
                    endGame();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            DoorInstruction.SetActive(false);
            TakeInstruction.SetActive(false);
            LockDoorInstruction.SetActive(false);
            SubmitInstruction.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            //ZamceneDvere - 5 ukol
            if (other.gameObject.CompareTag("LockDoor"))
            {
                if (tryDoor && doorKey==false)
                {
                    LockDoorInstruction.SetActive(true);
                }
                else 
                {
                    DoorInstruction.SetActive(true);
                }
            }
            //Odevzdej - 4 ukol
            if (other.gameObject.CompareTag("Pepper"))
            {
                SubmitInstruction.SetActive(true);
            }
            //Stul - 10 ukol
            if (other.gameObject.CompareTag("Stul"))
            {
                LayInstruction.SetActive(true);
            }
        }

        private void Update()
        {

            napoveda.text = napovedy[zobrazNapovedu()];

            if (Input.GetKeyDown(KeyCode.Escape) && gameDone) {
                finish.SetActive(false);
                Time.timeScale = 1f;
                PauseMenu.gameIsDone = false;
            }

        }

        private bool Kontrola() {
            for (int i = 0; i < quests.Length; i++)
            {
                if (quests[i] != true)
                {
                    return false;
                }
            }
            return true;
        }

        private int zobrazNapovedu() {
            for (int i = 0; i < quests.Length; i++) {
                if (!quests[i]) {
                    return i;
                }
            }
            return 0;
        }

        void endGame()
        {
            int m = timeCounter.GetComponent<TimeCounter>().getMin();
            int s = timeCounter.GetComponent<TimeCounter>().getSec();
            finishTime.text = m.ToString("D2") + ":" + s.ToString("D2");
            timeCounter.GetComponent<TimeCounter>().StopTime();
            gameDone = true;
            PauseMenu.gameIsDone = true;
            finish.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
