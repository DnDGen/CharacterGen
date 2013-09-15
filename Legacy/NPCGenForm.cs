using NPCGen.Characters;
using NPCGen.Roll;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            RaceCombo.Items.AddRange(Races.RaceRandomizerArray);
            MetaRaceCombo.Items.AddRange(Races.MetaRacesArray);
            MetaRaceCombo.Items.AddRange(Races.MetaRaceRandomizerArray);

            ClassCombo.Items.AddRange(Classes.ClassesArray);
            ClassCombo.Items.AddRange(Classes.ClassRandomizerArray);

            ClassLvlCombo.Items.AddRange(Classes.LevelRandomizerArray);
            for (var i = 1; i <= 40; i++)
                ClassLvlCombo.Items.Add(i);

            GenderCombo.Items.Add("Any");
            GenderCombo.Items.Add("Male");
            GenderCombo.Items.Add("Female");

            RollMethodCombo.Items.AddRange(StatDice.RollMethodArray);
            RollMethodCombo.Items.Add("ANY");

            AlignmentCombo.Items.AddRange(Character.AlignmentRandomizerArray);
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

            var RollMethod = ROLLMETHOD.STRAIGHT;
            var Race = new Races();
            var Class = CLASS.FIGHTER; 
            var Level = 1;
            var Alignment = new ALIGNMENT[2];
            Character NPC;
            Boolean Male;

            //Determine the alignment
            Application.DoEvents();
            var Randomized = false;
            OutputText.Text += "\nDetermining Alignment...";
            for (var i = 0; i < Character.AlignmentRandomizerArray.Length; i++)
            {
                if (Character.AlignmentRandomizerArray[i] == AlignmentCombo.Text)
                {
                    Alignment = Character.RandomAlignment((ALIGNMENT_RANDOMIZER)i);
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
            for (var i = 0; i < Classes.ClassRandomizerArray.Length; i++)
            {
                if (Classes.ClassRandomizerArray[i] == ClassCombo.Text)
                {
                    Class = Classes.RandomClass((CLASSRANDOMIZER)i, Alignment);
                    Randomized = true;
                    break;
                }
            }

            //set class, if not randomized
            if (!Randomized)
            {
                for (var i = 0; i < Classes.ClassesArray.Length; i++)
                {
                    if (Classes.ClassesArray[i] == ClassCombo.Text)
                    {
                        Class = (CLASS)i;
                        break;
                    }
                }
            }
            OutputText.Text += Class.ToString();

            //verify correct alignments
            if (Class == CLASS.PALADIN || Class == CLASS.MONK)
                Alignment[0] = ALIGNMENT.LAWFUL;
            else if (Alignment[0] == ALIGNMENT.LAWFUL && (Class == CLASS.BARBARIAN || Class == CLASS.BARD))
                Alignment[0] = ALIGNMENT.NEUTRAL;
            else if (Class == CLASS.DRUID && !(Alignment[0] == ALIGNMENT.NEUTRAL || Alignment[1] == ALIGNMENT.NEUTRAL))
                Alignment[0] = ALIGNMENT.NEUTRAL;

            //Determine level by randomization method
            Application.DoEvents();
            OutputText.Text += "\nDetermining Level...";
            Randomized = false;
            for (var i = 0; i < Classes.LevelRandomizerArray.Length; i++)
            {
                if (Classes.LevelRandomizerArray[i] == ClassLvlCombo.Text)
                {
                    Level = Classes.RandomLevel((LEVELRANDOMIZER)i);
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
            var Any = true;
            for (var i = 0; i < StatDice.RollMethodArray.Length; i++)
            {
                if (StatDice.RollMethodArray[i] == RollMethodCombo.Text)
                {
                    RollMethod = (ROLLMETHOD)i;
                    Any = false;
                    break;
                }
            }
            if (Any)
                RollMethod = (ROLLMETHOD)Dice.Roll(1, StatDice.RollMethodArray.Length);

            OutputText.Text += RollMethod.ToString();

            //Determine the Race & Gender
            Application.DoEvents();
            OutputText.Text += "\nDetermining Race and Gender...";
            if (GenderCombo.Text == "Any")
            {
                Male = true;
                if (Dice.Percentile() < 34)
                    Male = false;
            }
            else if (GenderCombo.Text == "Female")
                Male = false;
            else
                Male = true;

            Randomized = false; 
            var AllowMetaRaces = false;
            for (var i = 0; i < Races.RaceRandomizerArray.Length; i++)
            {
                if (Races.RaceRandomizerArray[i] == RaceCombo.Text)
                {
                    if (MetaRaceCombo.Text == "MAYBE")
                        AllowMetaRaces = true;
                    Race = new Races(Alignment[1], Class, (RACE_RANDOMIZER)i, AllowMetaRaces, Male);
                    Randomized = true;
                    break;
                }
            }

            if (!Randomized)
            {
                for (var i = 0; i < Races.RacesArray.Length; i++)
                {
                    if (Races.RacesArray[i] == RaceCombo.Text)
                    {
                        Race.Race = (RACE)i;
                        break;
                    }
                }
            }

            //Determine the metarace, if any
            Randomized = false;
            for (var i = 0; i < Races.MetaRaceRandomizerArray.Length; i++)
            {
                if (Races.MetaRaceRandomizerArray[i] == MetaRaceCombo.Text)
                {
                    Race.MetaRace = Races.RandomMetaRace(Alignment[1], Class, (METARACE_RANDOMIZER)i);
                    Randomized = true;
                    break;
                }
            }

            if (!Randomized && MetaRaceCombo.Text != "NONE" && MetaRaceCombo.Text != "MAYBE")
            {
                for (var i = 0; i < Races.MetaRacesArray.Length; i++)
                {
                    if (Races.MetaRacesArray[i] == MetaRaceCombo.Text)
                    {
                        Race.MetaRace = (METARACE)i;
                        break;
                    }
                }
            }
            OutputText.Text += Race.ToString();

            //Create the NPC and output to form
            Application.DoEvents();
            OutputText.Text += "\nCreating NPC...";
            NPC = new Character(RollMethod, Race, Alignment, Class, Level, ref OutputText);
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
            OutputText.Text = String.Empty;
        }
    }
}