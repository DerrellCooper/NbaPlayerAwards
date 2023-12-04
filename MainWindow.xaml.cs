using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NBA_Player_Awards {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        struct PlayerStats {
            public string Name;
            public string Team;
            public int Rookie;
            public double Rating;
            public double GamesPlayed;
            public double MinutesPerGame;
            public double PointsPerGame;
            public double reboundsPerGame;
            public double AssistsPerGame;
            public double ShotPercentage;
            public double FreethrowPercentage;
        }//END STRUCT

        //create an array to hold all stats
        PlayerStats[] stats;
        public MainWindow() {
            InitializeComponent();

            string csvFilePath = @"C:\Users\MCA\source\repos\NBA_Awards\bin\Debug\net6.0-windows\NBA_DATA.csv";
            int numberOfPlayers = CountCsvRecords(csvFilePath, true);

            stats = ProcessCsvData(csvFilePath, numberOfPlayers, true);
            int numberOfEligiblePlayers = NumberOfEligiblePlayers(csvFilePath, true);

            PriusAwardWinner(numberOfEligiblePlayers);
            GasGuzzlerAwardWinner(numberOfEligiblePlayers);
            FoulTargetAqardWinner(numberOfEligiblePlayers);
            BangForBuckAward(numberOfEligiblePlayers);
            GordonGekkoAwardWinner(numberOfEligiblePlayers);
            OverUnderAchieverAwardWinners(numberOfEligiblePlayers);
        }
        // CREATE STRUCT TO HOLD DATA FROM CSV FILE
        

        #region events
        void Window_Activated(object sender, EventArgs e) {
            
        }//end event

        private void Button_Click(object sender, RoutedEventArgs e) {
            Charlie_Brown_Awards win = new Charlie_Brown_Awards();
            win.Show();
        }//END EVENT
        #endregion

        #region functions
        int CountCsvRecords(string filePath, bool skipHeader) {
            int recordCount = 0;

            StreamReader infile = new StreamReader(filePath);

            if (skipHeader) {
                infile.ReadLine();
            }//end if

            while (infile.EndOfStream == false) {
                infile.ReadLine();
                recordCount++;
            }//end while

            infile.Close();

            return recordCount;
        }
        int NumberOfEligiblePlayers(string filePath, bool skipHeader) {
            int recordCount = 0;
            StreamReader infile = new StreamReader(filePath);
            if (skipHeader) {
                infile.ReadLine();
            }//end if

            int eligibleCount = 0;
            while (infile.EndOfStream == false) {
                string record = infile.ReadLine();
                string[] fields = record.Split(",");
                if (double.Parse(fields[4]) > 0) {
                    eligibleCount++;
                }//end if
            }//end while
            return eligibleCount;
        }//end function

        //populate stats array with csv file
        PlayerStats[] ProcessCsvData(string filePath, int recordsToRead, bool skipHeader) {
            PlayerStats[] returnArray = new PlayerStats[recordsToRead];
            int currentRecordCount = 0;

            StreamReader inFile = new StreamReader(filePath);

            if (skipHeader) {
                inFile.ReadLine();
            }//end if
            int returnArrayIndex = 0;
            while (inFile.EndOfStream == false && currentRecordCount < recordsToRead) {
                string record = inFile.ReadLine();
                string[] fields = record.Split(",");


                if (double.Parse(fields[4]) > 0) {
                    returnArray[returnArrayIndex].Name = fields[0];
                    returnArray[returnArrayIndex].Team = fields[1];
                    returnArray[returnArrayIndex].Rookie = int.Parse(fields[2]);
                    returnArray[returnArrayIndex].Rating = double.Parse(fields[3]);
                    returnArray[returnArrayIndex].GamesPlayed = double.Parse(fields[4]);
                    returnArray[returnArrayIndex].MinutesPerGame = double.Parse(fields[5]);
                    returnArray[returnArrayIndex].PointsPerGame = double.Parse(fields[6]);
                    returnArray[returnArrayIndex].reboundsPerGame = double.Parse(fields[7]);
                    returnArray[returnArrayIndex].AssistsPerGame = double.Parse(fields[8]);
                    returnArray[returnArrayIndex].ShotPercentage = double.Parse(fields[9]);
                    returnArray[returnArrayIndex].FreethrowPercentage = double.Parse(fields[10]);
                    returnArrayIndex++;
                }//end if


                currentRecordCount++;


            }//end while


            inFile.Close();

            return returnArray;
        }//end function

        // Prius Award
        void PriusAwardWinner(int numberOfRecords) {
            string[] playerNameArray = new string[numberOfRecords];
            double[] avgPointsPerMin = new double[numberOfRecords];
            
            for (int index = 0; index < numberOfRecords; index++) {

                playerNameArray[index] = stats[index].Name;
                avgPointsPerMin[index] = (stats[index].PointsPerGame * stats[index].GamesPlayed) / (stats[index].MinutesPerGame * stats[index].GamesPlayed);
                
            }//end for
            ReOrderStatsLeastToGreatest(playerNameArray, avgPointsPerMin);
            

            priusAward.Text = $"{playerNameArray[playerNameArray.GetLength(0) - 1]} " +
                $"({avgPointsPerMin[avgPointsPerMin.GetLength(0) - 1]} Points Per Minute)";

            bool isTied = true;
            while (isTied) {

                for (int index = playerNameArray.GetLength(0) - 2; index > 0; index--) {
                    if (avgPointsPerMin[index + 1] == avgPointsPerMin[index]) {
                        bangBuckAward.Text += $"\n{playerNameArray[index]} ({avgPointsPerMin[index]} minutes)";
                        isTied = true;
                    } else {
                        isTied = false; break;
                    }//END ELSE IF 
                }//END FOR
            }//END WHILE
        }//end function
        void GasGuzzlerAwardWinner(int numberOfRecords) {
                string[] playerNameArray = new string[numberOfRecords];
                double[] gameTimeArray = new double[numberOfRecords];
                double[] avgPointsPerMin = new double[numberOfRecords];
                //populate Arrays
                for (int index = 0; index < numberOfRecords; index++) {

                    playerNameArray[index] = stats[index].Name;
                    avgPointsPerMin[index] = (stats[index].PointsPerGame * stats[index].GamesPlayed) / (stats[index].MinutesPerGame * stats[index].GamesPlayed);

            }//end for

            ReOrderStatsLeastToGreatest(playerNameArray, gameTimeArray);

            gasGuzzlerAward.Text = $"{playerNameArray[0]} " +
                $"({avgPointsPerMin[0]:f} Points Per Minute)";

            bool isTied = true;
            while (isTied) {

                for (int index = 1; index < playerNameArray.GetLength(0) - 1; index++) {
                    if (avgPointsPerMin[index - 1] == avgPointsPerMin[index]) {
                        gasGuzzlerAward.Text += $"\n{playerNameArray[index]} ({avgPointsPerMin[index]:f})";
                        isTied = true;
                    } else {
                        isTied = false; break;
                    }//END ELSE IF 
                }//END FOR
            }//END WHILE
        }//end function

        void FoulTargetAqardWinner(int numberOfRecords) {
            string[] playerNameArray = new string[numberOfRecords];
            double[] avgArray = new double[numberOfRecords];
            //populate Arrays
            for (int index = 0; index < numberOfRecords; index++) {

                playerNameArray[index] += stats[index].Name;
                avgArray[index] += stats[index].FreethrowPercentage / stats[index].ShotPercentage;

            }//end for

            string nameHolder;
           
            ReOrderStatsLeastToGreatest(playerNameArray, avgArray);
            foulTargetAward.Text = $"{playerNameArray[playerNameArray.GetLength(0) - 1]} ({avgArray[avgArray.GetLength(0) - 1]})";

            bool isTied = true;
            while (isTied) {

                for (int index = playerNameArray.GetLength(0) - 2; index > 0; index--) {
                    if (avgArray[index + 1] == avgArray[index]) {
                        foulTargetAward.Text += $"\n{playerNameArray[index]} ({avgArray[index]:f})";
                        isTied = true;
                    } else {
                        isTied = false; break;
                    }//END ELSE IF 
                }//END FOR
            }//END WHILE
        }//end function

        void BangForBuckAward (int numOfPlayers) {
            string[] rookieNames = new string[numOfPlayers];
            double[] totalPlayTime = new double[numOfPlayers];

            for (int index = 0; (index < numOfPlayers); index++) {
                if (stats[index].Rookie == 1) {
                    rookieNames[index] = stats[index].Name;
                    totalPlayTime[index] = stats[index].MinutesPerGame * stats[index].GamesPlayed;
                }//end if
            }//end for

            ReOrderStatsLeastToGreatest (rookieNames, totalPlayTime);
            bangBuckAward.Text = $"{rookieNames[rookieNames.GetLength(0) - 1]} " +
                $"({totalPlayTime[totalPlayTime.GetLength(0) - 1]} Minutes)";
            bool isTied = true;
            while (isTied) {

                for (int index = rookieNames.GetLength(0) - 2; index > 0 ; index--) {
                    if (totalPlayTime[index + 1] == totalPlayTime[index]) {
                        bangBuckAward.Text += $"\n{rookieNames[index]} ({totalPlayTime[index]} minutes)";
                        isTied = true;
                    } else {
                        isTied = false; break;
                    }//END ELSE IF 
                }//END FOR
            }//END WHILE
        }//end function

        void GordonGekkoAwardWinner(int numberOfRecords) {
            string[] divisionName = { "Atlantic", "Central", "Pacific", "Southeast", "Northwest", "Southwest" };
            double[] divisionTotalPoints = new double[divisionName.Length];
            //calculate eawch region's points
            for (int index = 0; index < numberOfRecords; index++) {
                if (stats[index].Team == "BOS" || stats[index].Team == "BKN" || stats[index].Team == "NYK" || stats[index].Team == "PHI" || stats[index].Team == "TOR") {
                    divisionTotalPoints[0] += (stats[index].PointsPerGame * stats[index].GamesPlayed);
                } else if (stats[index].Team == "CHI" || stats[index].Team == "CLE" || stats[index].Team == "DET" || stats[index].Team == "IND" || stats[index].Team == "MIL") {
                    divisionTotalPoints[1] += (stats[index].PointsPerGame * stats[index].GamesPlayed);
                } else if (stats[index].Team == "GSW" || stats[index].Team == "LAC" || stats[index].Team == "LAL" || stats[index].Team == "PHX" || stats[index].Team == "SAC") {
                    divisionTotalPoints[2] += (stats[index].PointsPerGame * stats[index].GamesPlayed);
                } else if (stats[index].Team == "ATL" || stats[index].Team == "CHA" || stats[index].Team == "MIA" || stats[index].Team == "ORL" || stats[index].Team == "WAS") {
                    divisionTotalPoints[3] += (stats[index].PointsPerGame * stats[index].GamesPlayed);
                } else if (stats[index].Team == "DEN" || stats[index].Team == "MIN" || stats[index].Team == "OKC" || stats[index].Team == "POR" || stats[index].Team == "UTA") {
                    divisionTotalPoints[4] += (stats[index].PointsPerGame * stats[index].GamesPlayed);
                } else if (stats[index].Team == "DAL" || stats[index].Team == "HOU" || stats[index].Team == "MEM" || stats[index].Team == "NOP" || stats[index].Team == "SAS") {
                    divisionTotalPoints[5] += (stats[index].PointsPerGame * stats[index].GamesPlayed);
                }//end else if
            }//end for
            
            //determine award winner
            ReOrderStatsLeastToGreatest(divisionName, divisionTotalPoints);
            gordonGekkoAward.Text = $"{divisionName[divisionName.Length - 1]} Division " +
                $"({divisionTotalPoints[divisionTotalPoints.GetLength(0) - 1]:f})";

        }//end function

        void OverUnderAchieverAwardWinners(int numberOfPlayers) {
            // collect player ratings
            string[] playerNames = new string[numberOfPlayers];
            double[] playerRating = new double[numberOfPlayers];

            for (int index = 0; index < playerNames.GetLength(0); index++) {
                playerNames[index] = stats[index].Name;
                playerRating[index] = stats[index].Rating;
            }//end for

            // reorder from least to greatest

           ReOrderStatsLeastToGreatest(playerNames, playerRating);

            overachiever1.Text = $"{playerNames[playerNames.GetLength(0) - 1]} ({playerRating[playerRating.GetLength(0)-1]})";
            overachiever2.Text = $"{playerNames[playerNames.GetLength(0) - 2]} ({playerRating[playerRating.GetLength(0) - 1]})";
            overachiever3.Text = $"{playerNames[playerNames.GetLength(0) - 3]} ({playerRating[playerRating.GetLength(0) - 1]})";

            // show underachievers

            underachiever1.Text = $"{playerNames[0]} ({playerRating[0]})";
            underachiever2.Text = $"{playerNames[1]} ({playerRating[1]})";
            underachiever3.Text = $"{playerNames[2]} ({playerRating[2]})";

            //show on the fence
            onTheFenceAward.Text = $"{ playerNames[216]} ({playerRating[216]})";
        }//end function

        void ReOrderStatsLeastToGreatest(string[] names, double[] stats) {
            string nameHolder;
            double statHolder;
            bool changeOrder = true;
            while (changeOrder == true) {

                changeOrder = false;

                for (int index = 0; (index < names.GetLength(0) - 1); index++) {
                    if (stats[index] > stats[index + 1]) {
                        //change name array
                        nameHolder = names[index + 1];
                        names[index + 1] = names[index];
                        names[index] = nameHolder;

                        //change points array
                        statHolder = stats[index + 1];
                        stats[index + 1] = stats[index];
                        stats[index] = statHolder;
                        changeOrder = true;
                    }//end if
                }//end for
            }//end while
        }//end function

        



        #endregion


    }
}
