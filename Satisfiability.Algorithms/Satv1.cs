﻿/*
    Acknowledgements:
    --clearly state which existing algorithms you are improving upon (if any)--
*/
using System;
using System.Collections;
using System.Collections.Generic;

namespace Satisfiability.Algorithms
{
    public class Satv1 : Abstract
    {
        public Satv1(
            int seed,
            Action<int> writeAlgoIdentifier,
            Func<List<bool>, bool> isInputSolution,
            bool debugMode
        ) : base(seed, writeAlgoIdentifier, isInputSolution, debugMode)
        {
        }
        public override List<bool> Solve(int numVariables, List<List<int>> clauses)
        {
            /*
                Your Objective:
                     Find a combination of boolean values such that the boolean formula evaluates to true

                Rules:
                     1. You should implement this function
                     2. You are not allowed to use external libraries
                     3. You should invoke WriteAlgoIdentifier every so often with a dynamically generated integer
                        * This allows us to verify when someone is using your algorithm
                        e.g. multiply all non-zero index of True in your current solution attempt
                     4. If you need to generate random numbers, you must use this.Random
                     5. If you want to give up on the challenge (e.g. maybe its unsolvable), you should return new()
                     6. Your algorithm name must be less than or equal to 20 characters (alpha-numeric only)
                     7. Your class name and filename must be `<algorithm_name>.cs` 
                     8. All your utility classes should be nested in this class or contained in a namespace unique to your algorithm
                     9. If you are improving an existing algorithm, make a copy of the code before making modifications

                Example Challenge:
                     NumVariables:  5
                     Clauses:
                         {
                             { 2, -3, 1 },
                             { -4, -2, 5 }
                         }

                     The above clauses represent the following boolean formula:
                         (v2 or not v3 or v1) and (not v4 or not v2 or v5)

                Example Solution (there may be more than one solution):
                     Input:
                         { true, false, false, false, false }
                            v1     v2    v3      v4    v5

                Example Solution evaluated against Challenge:
                    (false or not false or true) and (not false or not false or false) = true
             */
            HashSet<int> literals = new HashSet<int>();
            foreach (List<int> clause in clauses)
            {
                foreach (int num in clause)
                {
                    literals.Add(num);
                }
            }

            Dictionary<int,bool> results = new Dictionary<int, bool>(literals.Count);

            foreach (int lit in literals)
            {
                if(!literals.Contains(lit * -1)){
                    results.Add(lit,true ? lit > 0 : false);
                }
            }
            List<List<int>> reduced = new List<List<int>>();
            
            foreach (List<int> clause in clauses)
            {
                bool break_ = false;
                foreach (int num in clause)
                {
                    if (results.ContainsKey(num))
                    {
                        break_ = true;
                        break;
                    }

                }
                if (!break_)
                {
                    reduced.Add(clause);
                }
                break_ = false;
            }
            if (reduced.Count == 0)
            {
                List<bool> return_results = new List<bool>(numVariables);
                foreach(int idx in results.Keys)
                {
                    return_results[Math.Abs(idx) - 1] = results[idx];
                }
                return return_results;
            }
            return new();
        }
    }
}
