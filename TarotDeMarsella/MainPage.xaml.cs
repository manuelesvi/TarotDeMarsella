namespace TarotDeMarsella;
using System.Globalization;
using Humanizer;

public partial class MainPage : ContentPage
{
    private readonly CultureInfo usCulture = new("en-US");

    public MainPage()
    {
        InitializeComponent();

        numberOfRows.Items.Add(1.ToWords());
        numberOfRows.Items.Add(2.ToWords());
        numberOfRows.Items.Add(3.ToWords());
        numberOfRows.SelectedIndex = 0;
        numberOfRows.SelectedIndexChanged += NumberOfRows_SelectedIndexChanged;

        LoadMayores();
    }

    private void NumberOfRows_SelectedIndexChanged(object sender, EventArgs e)
    {
        row1.Children.Clear();
        row2.Children.Clear();
        row3.Children.Clear();

        switch (numberOfRows.SelectedIndex)
        {
            case 0:
                row2.IsVisible = false;
                row3.IsVisible = false;
                break;
            case 1:
                row2.IsVisible = true;
                row3.IsVisible = false;
                break;
            case 2:
                row2.IsVisible = true;
                row3.IsVisible = true;
                break;
        }        

        LoadMayores();
        
    }

    private ImageSource LoadArcanoMayor(int number)
    {
        string number_word = number.ToWords(usCulture).Replace('-', '_');
        return ImageSource.FromFile($"arcanomayor_{number_word}.jpg");
    }

    private void LoadMayores()
    {
        int numRows = numberOfRows.SelectedIndex + 1;
        int row = 0;
        do
        {
            int startI, endI;
            if (numRows == 1)
            {
                startI = 0;
                endI = 21;
            }
            else if (numRows == 2)
            {
                if (row == 0)
                {
                    startI = 1;
                    endI = 10;
                }
                else // 1
                {
                    startI = 11;
                    endI = 20;
                }
            }
            else
            {
                // 1..7, 8..14, 15..21
                startI = 1 + 7 * row;
                endI = startI + 6;
            }

            for (int i = startI; i <= endI; i++)
            {
                HorizontalStackLayout container;
                switch (row)
                {
                    case 0:
                    default:
                        container = row1; break;
                    case 1:
                        container = row2; break;
                    case 2:
                        container = row3; break;
                }

                container.Add(new Image
                {
                    Source = LoadArcanoMayor(i)
                });
            }
        } while (++row < numRows);        
    }
}

