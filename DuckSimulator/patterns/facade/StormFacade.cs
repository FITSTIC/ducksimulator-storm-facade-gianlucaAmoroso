using System;
using System.Collections.Generic;

/// <summary>
/// Interfaccia del sistema Stormo.
/// Uno stormo viene riempito di papere e poi viene fatto migrare verso una direzione.
/// </summary>
public interface IStorm<Tduck> where Tduck : Duck, IFlyable, new()
{
    /// <summary>
    /// Lista delle papere che compongono lo stormo
    /// </summary>
    /// <returns></returns>
    List<Tduck> Ducks { get; }
    /// <summary>
    /// Riempie lo stormo
    /// </summary>
    /// <param name="nDucks">numero di papere che compongono lo stormo</param>
    void FillStorm(int nDucks);
    /// <summary>
    /// Posizione corrente sulle cordinate X dello stormo
    /// </summary>
    /// <returns></returns>
    double PositionX { get; }
    /// <summary>
    /// Posizione corrente sulle cordinate Y dello stormo
    /// </summary>
    /// <returns></returns>
    double PositionY { get; }
    /// <summary>
    /// Contatore totale dello spazio percorso dallo stormo durante la migrazione.
    /// </summary>
    /// <returns></returns>
    double TotalDistance { get; }
    /// <summary>
    /// Fa migrare lo stormo verso una direzione
    /// </summary>
    /// <param name="dir">Direzione verso la quale tutto lo stormo sta migrando</param>
    /// <param name="distance">Distanza che deve percorrere lo stormo</param>
    void Migrate(Direction dir, double distance);
    /// <summary>
    /// Distanza in linea d'aria dal punto di partenza.
    /// </summary>
    /// <returns></returns>
    double LineDistanceFromStart { get; }
}

public class Storm<Tduck> : IStorm<Tduck> where Tduck : Duck, IFlyable, new()
{
    private List<Tduck> papere = new List<Tduck>();
    private double posX, posY, totDist;

    public List<Tduck> Ducks => papere;

    public double PositionX => Math.Round(posX, 1);

    public double PositionY => Math.Round(posY, 1);

    public double TotalDistance => Math.Round(totDist, 1);

    public double LineDistanceFromStart
    {
        get
        {
            return Math.Round(Math.Sqrt(Math.Pow(posX, 2) + Math.Pow(posY, 2)), 2);
        }
    }

    public void FillStorm(int nDucks)
    {
        if(nDucks < 1)
        {
            throw new ArgumentException("Impossibile inserire meno di una papera!");
        }

        for (int i = 0; i < nDucks; i++)
        {
            papere.Add(new Tduck());
        }
    }

    public void Migrate(Direction dir, double distance)
    {
        if(papere == null || papere.Count == 0)
        {
            throw new ApplicationException("Prima di usare uno stormo riempilo di papere!");
        }

        switch (dir)
        {
            case Direction.NORD:
                posY += distance;
                break;
            case Direction.SUD:
                posY += distance * -1;
                break;
            case Direction.OVEST:
                posX += distance * -1;
                break;
            case Direction.EST:
                posX += distance;
                break;
            default: throw new Exception("Direzione indicata non valida!");
        }

        totDist += distance;

        foreach(Tduck p in papere)
        {
            p.Fly(distance);
            p.CurrentDirection = dir;
        }

    }
}