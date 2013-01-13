using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NPCGen
{
    public partial class NPCGenForm : Form
    { 
        public NPCGenForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RaceCombo.Items.AddRange(Races.RacesArray);
            RaceCombo.Items.AddRange(Enum.GetNames(typeof(Races.RACE_RANDOMIZER)));
            MetaRaceCombo.Items.AddRange(Enum.GetNames(typeof(Races.METARACE)));
            MetaRaceCombo.Items.AddRange(Enum.GetNames(typeof(Races.METARACE_RANDOMIZER)));

            ClassCombo.Items.AddRange(Classes.ClassesArray);
            ClassCombo.Items.AddRange(Classes.ClassRandomizerArray);
            ClassLvlCombo.Items.AddRange(Classes.LevelRandomizerArray);
            for (int i = 1; i <= 40; i++)
                ClassLvlCombo.Items.Add(i);

            GenderCombo.Items.Add("Any");
            GenderCombo.Items.Add("Male");
            GenderCombo.Items.Add("Female");

            RollMethodCombo.Items.AddRange(Dice.RollMethodArray);
            RollMethodCombo.Items.Add("ANY");

            AlignmentCombo.Items.AddRange(Enum.GetNames(typeof(Character.ALIGNMENT_RANDOMIZER)));
            AlignmentCombo.Items.Add("Lawful Good");
            AlignmentCombo.Items.Add("Neutral Good");
            AlignmentCombo.Items.Add("Chaotic Good");
            AlignmentCombo.Items.Add("Lawful Neutral");
            AlignmentCombo.Items.Add("True Neutral");
            AlignmentCombo.Items.Add("Chaotic Neutral");
            AlignmentCombo.Items.Add("Lawful Evil");
            AlignmentCombo.Items.Add("Neutral Evil");
            AlignmentCombo.Items.Add("Chaotic Evil");

            Reset();
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            GenerateButton.Enabled = false;
            ResetButton.Enabled = false;
            OutputText.Text = "Generating NPC...";

            Dice.ROLLMETHOD RollMethod = Dice.ROLLMETHOD.STRAIGHT; Races Race = new Races(); Classes.CLASS Class = Classes.CLASS.FIGHTER; int Level = 1;
            Character.ALIGNMENT[] Alignment = new Character.ALIGNMENT[2];
            Character NPC; Random random = new Random(); bool Male;

            //Determine the alignment
            Application.DoEvents();
            bool Randomized = false;
            OutputText.Text += "\nDetermining Alignment...";
            for (int i = 0; i < Character.AlignmentRandomizerArray.Length; i++)
            {
                if (Character.AlignmentRandomizerArray[i] == AlignmentCombo.Text)
                {
                    Alignment = Character.RandomAlignment((Character.ALIGNMENT_RANDOMIZER)i, ref random);
                    Randomized = true;
                    break;
                }
            }

            if (!Randomized)
                Alignment = Character.StringToAlignment(AlignmentCombo.Text);
            OutputText.Text += Alignment[0].ToString() + Alignment[1].ToString();

            //Determine the class
            //Determine class by randomization method
            Application.DoEvents();
            OutputText.Text += "\nDetermining Class...";
            Randomized = false;
            for (int i = 0; i < Classes.ClassRandomizerArray.Length; i++)
            {
                if (Classes.ClassRandomizerArray[i] == ClassCombo.Text)
                {
                    Class = Classes.RandomClass((Classes.CLASSRANDOMIZER)i, Alignment, ref random);
                    Randomized = true;
                    break;
                }
            }

            //set class, if not randomized
            if (!Randomized)
            {
                for (int i = 0; i < Classes.ClassesArray.Length; i++)
                {
                    if (Classes.ClassesArray[i] == ClassCombo.Text)
                    {
                        Class = (Classes.CLASS)i;
                        break;
                    }
                }
            }
            OutputText.Text += Class.ToString();

            //verify correct alignments
            if (Class == Classes.CLASS.PALADIN || Class == Classes.CLASS.MONK)
                Alignment[0] = Character.ALIGNMENT.LAWFUL;
            else if (Alignment[0] == Character.ALIGNMENT.LAWFUL && (Class == Classes.CLASS.BARBARIAN || Class == Classes.CLASS.BARD))
                Alignment[0] = Character.ALIGNMENT.NEUTRAL;
            else if (Class == Classes.CLASS.DRUID && !(Alignment[0] == Character.ALIGNMENT.NEUTRAL || Alignment[1] == Character.ALIGNMENT.NEUTRAL))
                Alignment[0] = Character.ALIGNMENT.NEUTRAL;

            //Determine level by randomization method
            Application.DoEvents();
            OutputText.Text += "\nDetermining Level...";
            Randomized = false;
            for (int i = 0; i < Classes.LevelRandomizerArray.Length; i++)
            {
                if (Classes.LevelRandomizerArray[i] == ClassLvlCombo.Text)
                {
                    Level = Classes.RandomLevel((Classes.LEVELRANDOMIZER)i, ref random);
                    Randomized = true;
                    break;
                }
            }

            //Set level if not randomized
            if (!Randomized)
                Level = Convert.ToInt16(ClassLvlCombo.Text);
            OutputText.Text += Level.ToString();

            //Determine the roll method
            Application.DoEvents();
            OutputText.Text += "\nDetermining Roll Method for stats...";
            bool Any = true;
            for (int i = 0; i < Dice.RollMethodArray.Length; i++)
            {
                if (Dice.RollMethodArray[i] == RollMethodCombo.Text)
                {
                    RollMethod = (Dice.ROLLMETHOD)i;
                    Any = false;
                    break;
                }
            }
            if (Any)
                RollMethod = (Dice.ROLLMETHOD)random.Next(1, Dice.RollMethodArray.Length);

            OutputText.Text += RollMethod.ToString();

            //Determine the Race & Gender
            Application.DoEvents();
            OutputText.Text += "\nDetermining Race and Gender...";
            if (GenderCombo.Text == "Any")
            {
                Male = true;
                if (Dice.Percentile(ref random) < 34)
                    Male = false;
            }
            else if (GenderCombo.Text == "Female")
                Male = false;
            else
                Male = true;

            Randomized = false; bool AllowMetaRaces = false;
            for (int i = 0; i < Races.RaceRandomizerArray.Length; i++)
            {
                if (Races.RaceRandomizerArray[i] == RaceCombo.Text)
                {
                    if (MetaRaceCombo.Text == "MAYBE")
                        AllowMetaRaces = true;
                    Race = new Races(Alignment[1], Class, (Races.RACE_RANDOMIZER)i, AllowMetaRaces, Male, ref random);
                    Randomized = true;
                    break;
                }
            }

            if (!Randomized)
            {
                for (int i = 0; i < Races.RacesArray.Length; i++)
                {
                    if (Races.RacesArray[i] == RaceCombo.Text)
                    {
                        Race.Race = (Races.RACE)i;
                        break;
                    }
                }
            }

            //Determine the metarace, if any
            Randomized = false;
            for (int i = 0; i < Races.MetaRaceRandomizerArray.Length; i++)
            {
                if (Races.MetaRaceRandomizerArray[i] == MetaRaceCombo.Text)
                {
                    Race.MetaRace = Races.RandomMetaRace(Alignment[1], Class, (Races.METARACE_RANDOMIZER)i, ref random);
                    Randomized = true;
                    break;
                }
            }

            if (!Randomized && MetaRaceCombo.Text != "NONE" && MetaRaceCombo.Text != "MAYBE")
            {
                for (int i = 0; i < Races.MetaRacesArray.Length; i++)
                {
                    if (Races.MetaRacesArray[i] == MetaRaceCombo.Text)
                    {
                        Race.MetaRace = (Races.METARACE)i;
                        break;
                    }
                }
            }
            OutputText.Text += Race.ToString();

            //Create the NPC and output to form
            Application.DoEvents();
            OutputText.Text += "\nCreating NPC...";
            NPC = new Character(RollMethod, Race, Alignment, Class, Level, ref OutputText, ref random);
            OutputText.Text = NPC.ToString();

            //re-enable other command tools
            GenerateButton.Enabled = true;
            ResetButton.Enabled = true;
            Application.DoEvents();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            RaceCombo.SelectedIndex = RaceCombo.FindString("Any");
            GenderCombo.SelectedIndex = GenderCombo.FindString("Any");
            ClassCombo.SelectedIndex = ClassCombo.FindString("ANY");
            ClassLvlCombo.SelectedIndex = ClassLvlCombo.FindString("ANY");
            RollMethodCombo.SelectedIndex = RollMethodCombo.FindString("ANY");
            MetaRaceCombo.SelectedIndex = MetaRaceCombo.FindString("NONE");
            AlignmentCombo.SelectedIndex = AlignmentCombo.FindString("ANY");
            OutputText.Text = "";
        }
    }
}