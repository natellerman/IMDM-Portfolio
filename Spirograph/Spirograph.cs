using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

internal class Spirograph
{
    public Window win;
    private int initHeight = 720;
    private int initWidth = 1280;

    private Canvas canvas;
    public Spirograph()
    {
        win = new Window
        {
            Title = "Spirograph v0.1",
            Height = initHeight,
            Width = initWidth,
            Background = Brushes.Magenta,
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
        };

        canvas = new Canvas
        {
            Background = Brushes.Black,
        };

        win.Content = canvas;

        win.Resized += Redraw;
        win.Show();
    }

    private void Redraw(object sender, WindowResizedEventArgs e)
    {
        float height = (float)win.Height;
        float width = (float)win.Width;

        canvas.Height = height;
        canvas.Width = width;

        float xCenter = width / 2f;
        float yCenter = height / 2f;

        canvas.Children.Clear();

        float R = height / 2f * 0.9f;
        float r = R / 10.5f;  // same frequency for both
        int numberOfLoops = 5;
        int stepsPerLoop = 1000;

        DrawSpirograph(xCenter, yCenter, R, r, r * 1.5f, numberOfLoops, stepsPerLoop, Brushes.HotPink);

        DrawSpirograph(xCenter, yCenter, R, r, r * 0.3f, numberOfLoops, stepsPerLoop, Brushes.Lime);
    }

        private void DrawSpirograph(float xCenter, float yCenter, float R, float r, float d,
                            int numberOfLoops, int stepsPerLoop, IBrush color)
    {
        float deltaTheta = 2 * (float)Math.PI / stepsPerLoop;
        int totalSteps = numberOfLoops * stepsPerLoop;

        Point[] points = new Point[totalSteps + 1];

        for (int step = 0; step <= totalSteps; step++)
        {
            float theta = step * deltaTheta;

            float x = (R - r) * (float)Math.Cos(theta) + d * (float)Math.Cos(((R - r) / r) * theta);
            float y = (R - r) * (float)Math.Sin(theta) - d * (float)Math.Sin(((R - r) / r) * theta);

            points[step] = new Point(x + xCenter, y + yCenter);
        }

        canvas.Children.Add(new Polyline
        {
            Stroke = color,
            StrokeThickness = 1.5,
            Points = points
        });
    }
}
