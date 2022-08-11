using System.Threading;
using Dova.JDK.Extensions;
using Dova.JDK.java.awt;
using Dova.JDK.javax.swing;
using Xunit;

namespace Dova.Examples.Tests;

public class JFrameTests : BaseTestClass
{
    [Fact]
    public void Should_create_simple_window()
    {
        Setup();

        var jframe = new JFrame("Simple Window using JNI".ToJava());
        jframe.setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE_Property);

        var jlabel = new JLabel("Sample Label using JNI".ToJava(), SwingConstants.CENTER_Property);
        jlabel.setPreferredSize(new Dimension(300, 100));

        jframe.getContentPane().add(jlabel, BorderLayout.CENTER_Property);

        jframe.setLocationRelativeTo(null);
        jframe.pack();
        jframe.setVisible(true);

        Thread.Sleep(5000);
    }
}