/**
 * Random quest generator.
 * This is based on the paper "A Prototype Quest Generator Based on a Structural 
 * Analysis of Quests from Four MMORPGs".
 * You can find this paper at http://larc.unt.edu/techreports/LARC-2011-02.pdf
 * If you do not read that paper, the generated quest may not make much sense.
 * 
 * The authors analyzed 3,000 quests from four RPGs and found that quests
 * generally could follow a predictable structure, though this structure was
 * often very complex.
 * 
 * The idea is to randomly generate quests based on multiple motivations. This is
 * a proof of concept for a game the author is writing. It's possible that I'll
 * expand this significantly for my personal work, or perhaps I'll use the
 * general information to inspiration instead of adhering too closely to the
 * output.
*/
package jzrlib.external.pqg;

/**
 *
 * @author Serg V. Zhdanovskih
 */
public final class QNode
{
    public final String description;
    public final String[] sequence;

    public QNode(String description, String... sequence)
    {
        this.description = description;
        this.sequence = sequence;
    }
}
