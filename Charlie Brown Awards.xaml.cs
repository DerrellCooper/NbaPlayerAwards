using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NBA_Player_Awards
{
    /// <summary>
    /// Interaction logic for Charlie_Brown_Awards.xaml
    /// </summary>
    public partial class Charlie_Brown_Awards : Window {
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

        //CREATE AN ARRAY TO HOLD ALL STATS
        PlayerStats[] stats;
        public Charlie_Brown_Awards()
        {
            InitializeComponent();
            string csvFilePath = @"C:\Users\MCA\source\repos\NBA_Awards\bin\Debug\net6.0-windows\NBA_DATA.csv";
            int numberOfPlayers = CountCsvRecords(csvFilePath, true);

            stats = ProcessCsvData(csvFilePath, numberOfPlayers, true);

            int numberOfEligiblePlayers = NumberOfEligiblePlayers(csvFilePath, true);
            //SHOW AWARDS

            //GamesPlayedCharlieTiger(numberOfPlayers);
            TotalPlayTimeCharlieTiger(numberOfEligiblePlayers);
            TotalPointsCharlieTiger(numberOfEligiblePlayers);
            TotalReboundsCharlieTiger(numberOfEligiblePlayers);
            TotalAssistsCharlieTiger(numberOfEligiblePlayers);
            ShotPercentageCharlieTiger(numberOfEligiblePlayers);
            FreeThrowPercentageCharlieTiger(numberOfEligiblePlayers);
        }

        #region events

        #endregion

        #region Functions
        int CountCsvRecords(string filePath, bool skipHeader) {
            int recordCount = 0;

            StreamReader infile = new StreamReader(filePath);

            if (skipHeader) {
                infile.ReadLine();
            }//END IF

            while (infile.EndOfStream == false) {
                infile.ReadLine();
                recordCount++;
            }//END WHILE

            infile.Close();

            return recordCount;
        }//END FUNCTION

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
        PlayerStats[] ProcessCsvData(string filePath, int recordsToRead, bool skipHeader) {
            PlayerStats[] returnArray = new PlayerStats[recordsToRead];
            int currentRecordCount = 0;

            StreamReader inFile = new StreamReader(filePath);

            if (skipHeader) {
                inFile.ReadLine();
            }//END IF
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


            }//END WHILE


            inFile.Close();

            return returnArray;
        }//END FUNCTION
        void TotalPlayTimeCharlieTiger(int numberOfPlayers) {
            string[] names = new string[numberOfPlayers];
            double[] totalPlaytime = new double[numberOfPlayers];

            //COLLECT STATS

            for (int index = 0; index < numberOfPlayers; index++) {
                names[index] = stats[index].Name;
                totalPlaytime[index] = (stats[index].GamesPlayed * stats[index].MinutesPerGame);
            }//END FOR

            ReOrderStatsLeastToGreatest(names, totalPlaytime);
            

            playTimeTiger.Text = $"Tiger Uppercut:\n{names[names.GetLength(0) - 1]} ({totalPlaytime[totalPlaytime.GetLength(0) - 1]:f} minutes)";
            playTimeCharlie.Text = $"Charlie Brown:\n{names[0]} ({totalPlaytime[0]:f})";
            // ADD NAMES IF THERE IS A TIE FOR TIGER UPPERCUT AWARD
            bool isTied = true;
            while (isTied) {
                for (int index = totalPlaytime.GetLength(0) - 1; index > 0; index--) {
                    if (totalPlaytime[index - 1] == totalPlaytime[index]) {
                        playTimeTiger.Text += $"\n{names[index - 1]} ({totalPlaytime[index - 1]:f} minutes)";
                        isTied = true ;
                    }else {
                        isTied = false; break ;
                    }//END ELSE IF
                }//END FOR
            }//END WHILE

            // ADD NAMES IF THERE IS A TIE FOR CHARLIE BROWN AWARD
            isTied = true;
            while (isTied) {
                
                for (int index = 1; index < names.GetLength(0) - 1; index++) {
                    if (totalPlaytime[index - 1] == totalPlaytime[index]) {
                        playTimeCharlie.Text += $"\n{names[index]} ({totalPlaytime[index]:f} minutes)";
                        isTied = true ;
                    }else {
                        isTied = false; break ;
                    }//END ELSE IF 
                }//END FOR
            }//END WHILE
        }//END FUNCTION
        void TotalPointsCharlieTiger(int numberOfPlayers) {
            
            string[] names = new string[numberOfPlayers];
            double[] totalPoints = new double[numberOfPlayers];
            int arrayIndex = 0;
            //COLLECT STATS

            for (int index = 0; index < numberOfPlayers; index++) {
                    names[index] = stats[index].Name;
                    totalPoints[index] = (stats[index].GamesPlayed * stats[index].PointsPerGame);
                    arrayIndex++;
            }//END FOR

            ReOrderStatsLeastToGreatest(names, totalPoints);

            totalPointsTiger.Text = $"Tiger Uppercut:\n{names[names.GetLength(0) - 1]} ({totalPoints[totalPoints.GetLength(0) - 1]:f})";
            totalPointsCharlie.Text = $"Charlie Brown:\n{names[0]} ({totalPoints[0]:f})";
            // ADD NAMES IF THERE IS A TIE FOR TIGER UPPERCUT AWARD
            bool isTied = true;
            while (isTied) {
                for (int index = totalPoints.GetLength(0) - 1; index > 0; index--) {
                    if (totalPoints[index - 1] == totalPoints[index]) {
                        totalPointsTiger.Text += $"\n{names[index - 1]} ({totalPoints[index - 1]:f})";
                        isTied = true;
                    } else {
                        isTied = false; break;
                    }//END ELSE IF
                }//END FOR
            }//END WHILE

            // ADD NAMES IF THERE IS A TIE FOR CHARLIE BROWN AWARD
            isTied = true;
            while (isTied) {

                for (int index = 0; index < names.GetLength(0) - 1; index++) {
                    if (totalPoints[index] == totalPoints[index+1]) {
                        totalPointsCharlie.Text += $"\n{names[index+1]} ({totalPoints[index + 1]:f})";
                        isTied = true;
                    } else {
                        isTied = false; break;
                    }//END ELSE IF 
                }//END FOR
            }//END WHILE
        }//END FUNCTION

        void TotalReboundsCharlieTiger(int numberOfPlayers) {
            string[] names = new string[numberOfPlayers];
            double[] totalRebounds = new double[numberOfPlayers];

            //COLLECT STATS

            for (int index = 0; index < numberOfPlayers; index++) {
                names[index] = stats[index].Name;
                totalRebounds[index] = (stats[index].GamesPlayed * stats[index].reboundsPerGame);
            }//END FOR

            ReOrderStatsLeastToGreatest(names, totalRebounds);

            totalReboundsTiger.Text = $"Tiger Uppercut:\n{names[names.GetLength(0) - 1]} " +
                                      $"({totalRebounds[totalRebounds.GetLength(0) - 1]:f})";
            totalReboundsCharlie.Text = $"Charlie Brown:\n{names[0]} ({totalRebounds[0]:f})";
            // ADD NAMES IF THERE IS A TIE FOR TIGER UPPERCUT AWARD
            bool isTied = true;
            while (isTied) {
                for (int index = totalRebounds.GetLength(0) - 1; index > 0; index--) {
                    if (totalRebounds[index - 1] == totalRebounds[index]) {
                        totalReboundsTiger.Text += $"\n{names[index - 1]} ({totalRebounds[index - 1]:f} )";
                        isTied = true;
                    } else {
                        isTied = false; break;
                    }//END ELSE IF
                }//END FOR
            }//END WHILE

            // ADD NAMES IF THERE IS A TIE FOR CHARLIE BROWN AWARD
            isTied = true;
            while (isTied) {

                for (int index = 1; index < names.GetLength(0) - 1; index++) {
                    if (totalRebounds[index - 1] == totalRebounds[index]) {
                        totalReboundsCharlie.Text += $"\n{names[index]} ({totalRebounds[index]:f})";
                        isTied = true;
                    } else {
                        isTied = false; break;
                    }//END ELSE IF 
                }//END FOR
            }//END WHILE
        }//END FUNCTION

        void TotalAssistsCharlieTiger(int numberOfPlayers) {
            string[] names = new string[numberOfPlayers];
            double[] totalAssists = new double[numberOfPlayers];

            //COLLECT STATS

            for (int index = 0; index < numberOfPlayers; index++) {
                names[index] = stats[index].Name;
                totalAssists[index] = (stats[index].GamesPlayed * stats[index].AssistsPerGame);
            }//END FOR
            ReOrderStatsLeastToGreatest(names, totalAssists);
            

            totalAssistsTiger.Text = $"Tiger Uppercut:\n{names[names.GetLength(0) - 1]}" +
                                     $" ({totalAssists[totalAssists.GetLength(0) - 1]:f})";
            totalAssistsCharlie.Text = $"Charlie Brown:\n{names[0]} ({totalAssists[0]:f})";
            // ADD NAMES IF THERE IS A TIE FOR TIGER UPPERCUT AWARD
            bool isTied = true;
            while (isTied) {
                for (int index = totalAssists.GetLength(0) - 1; index > 0; index--) {
                    if (totalAssists[index - 1] == totalAssists[index]) {
                        totalAssistsTiger.Text += $"\n{names[index - 1]} ({totalAssists[index - 1]:f})";
                        isTied = true;
                    } else {
                        isTied = false; break;
                    }//END ELSE IF
                }//END FOR
            }//END WHILE

            // ADD NAMES IF THERE IS A TIE FOR CHARLIE BROWN AWARD
            isTied = true;
            while (isTied) {

                for (int index = 1; index < names.GetLength(0) - 1; index++) {
                    if (totalAssists[index - 1] == totalAssists[index]) {
                        totalAssistsCharlie.Text += $"\n{names[index]} ({totalAssists[index]:f})";
                        isTied = true;
                    } else {
                        isTied = false; break;
                    }//END ELSE IF 
                }//END FOR
            }//END WHILE
        }//END FUNCTION

        void ShotPercentageCharlieTiger(int numberOfPlayers) {

            string[] names = new string[numberOfPlayers];
            double[] shotPercentage = new double[numberOfPlayers];

            //COLLECT STATS

            for (int index = 0; index < numberOfPlayers; index++) {
                names[index] = stats[index].Name;
                shotPercentage[index] = stats[index].ShotPercentage;
            }//END FOR

            ReOrderStatsLeastToGreatest(names, shotPercentage);
            
            shotPercentageTiger.Text = $"Tiger Uppercut:\n{names[names.GetLength(0) - 1]} " +
                                       $"({shotPercentage[shotPercentage.GetLength(0) - 1]}%)";

            shotPercentageCharlie.Text = $"Charlie Brown:\n{names[0]} ({shotPercentage[0]}%)";
            // ADD NAMES IF THERE IS A TIE FOR TIGER UPPERCUT AWARD
            bool isTied = true;
            while (isTied) {
                for (int index = shotPercentage.GetLength(0) - 1; index > 0; index--) {
                    if (shotPercentage[index - 1] == shotPercentage[index]) {
                        shotPercentageTiger.Text += $"\n{names[index - 1]} ({shotPercentage[index - 1]}%)";
                        isTied = true;
                    } else {
                        isTied = false; break;
                    }//END ELSE IF
                }//END FOR
            }//END WHILE

            // ADD NAMES IF THERE IS A TIE FOR CHARLIE BROWN AWARD
            isTied = true;
            while (isTied) {

                for (int index = 1; index < names.GetLength(0) - 1; index++) {
                    if (shotPercentage[index - 1] == shotPercentage[index]) {
                        shotPercentageCharlie.Text += $"\n{names[index]} ({shotPercentage[index]}%)";
                        isTied = true;
                    } else {
                        isTied = false; break;
                    }//END ELSE IF 
                }//END FOR
            }//END WHILE
        }//END FUNCTION

        void FreeThrowPercentageCharlieTiger(int numberOfPlayers) {
            string[] names = new string[numberOfPlayers];
            double[] freeThrowPercentage = new double[numberOfPlayers];

            //COLLECT STATS

            for (int index = 0; index < numberOfPlayers; index++) {
                names[index] = stats[index].Name;
                freeThrowPercentage[index] = stats[index].FreethrowPercentage;
            }//END FOR

            ReOrderStatsLeastToGreatest(names, freeThrowPercentage);
            

            freeThrowTiger.Text = $"Tiger Uppercut:\n{names[names.GetLength(0) - 1]} " +
                                  $"({freeThrowPercentage[freeThrowPercentage.GetLength(0) - 1]}%)";

            freeThrowCharlie.Text = $"Charlie Brown: {names[0]} ({freeThrowPercentage[0]}%)";
            // ADD NAMES IF THERE IS A TIE FOR TIGER UPPERCUT AWARD
            bool isTied = true;
            while (isTied) {
                for (int index = freeThrowPercentage.GetLength(0) - 1; index > 0; index--) {
                    if (freeThrowPercentage[index - 1] == freeThrowPercentage[index]) {
                        freeThrowTiger.Text += $"\n{names[index - 1]} ({freeThrowPercentage[index - 1]}%)";
                        isTied = true;
                    } else {
                        isTied = false; break;
                    }//END ELSE IF
                }//END FOR
            }//END WHILE

            // ADD NAMES IF THERE IS A TIE FOR CHARLIE BROWN AWARD
            isTied = true;
            while (isTied) {

                for (int index = 1; index < names.GetLength(0) - 1; index++) {
                    if (freeThrowPercentage[index - 1] == freeThrowPercentage[index]) {
                        freeThrowCharlie.Text += $"\n{names[index]} ({freeThrowPercentage[index]}%)";
                        isTied = true;
                    } else {
                        isTied = false; break;
                    }//END ELSE IF 
                }//END FOR
            }//END WHILE
        }//END FUNCTION

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
    }//END PARTIAL CLASS
}//END NAMESPACE
