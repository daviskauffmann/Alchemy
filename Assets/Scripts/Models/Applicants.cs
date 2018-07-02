using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemy.Models {
    [Serializable]
    public class Applicants {
        [SerializeField]
        List<Apothecary> apothecaries;
        [SerializeField]
        List<Guard> guards;
        [SerializeField]
        List<Herbalist> herbalists;
        [SerializeField]
        List<Shopkeeper> shopkeepers;

        public event EventHandler<IntEventArgs> CountChanged;

        public Applicants() {
            apothecaries = new List<Apothecary>();
            guards = new List<Guard>();
            herbalists = new List<Herbalist>();
            shopkeepers = new List<Shopkeeper>();
        }

        public List<Apothecary> Apothecaries {
            get { return apothecaries; }
        }

        public List<Guard> Guards {
            get { return guards; }
        }

        public List<Herbalist> Herbalists {
            get { return herbalists; }
        }

        public List<Shopkeeper> Shopkeepers {
            get { return shopkeepers; }
        }

        public Employee[] Total {
            get {
                var total = new List<Employee>();
                
                foreach (var apothecary in Apothecaries) {
                    total.Add(apothecary);
                }

                foreach (var guard in Guards) {
                    total.Add(guard);
                }

                foreach (var herbalist in Herbalists) {
                    total.Add(herbalist);
                }

                foreach (var shopkeeper in Shopkeepers) {
                    total.Add(shopkeeper);
                }

                return total.ToArray();
            }
        }

        public void Add(Employee applicant) {
            if (applicant is Apothecary) {
                apothecaries.Add((Apothecary)applicant);
            } else if (applicant is Guard) {
                guards.Add((Guard)applicant);
            } else if (applicant is Herbalist) {
                herbalists.Add((Herbalist)applicant);
            } else if (applicant is Shopkeeper) {
                shopkeepers.Add((Shopkeeper)applicant);
            }

            OnCountChanged(Total.Length);
        }

        public void Remove(Employee applicant) {
            if (applicant is Apothecary) {
                apothecaries.Remove((Apothecary)applicant);
            } else if (applicant is Guard) {
                guards.Remove((Guard)applicant);
            } else if (applicant is Herbalist) {
                herbalists.Remove((Herbalist)applicant);
            } else if (applicant is Shopkeeper) {
                shopkeepers.Remove((Shopkeeper)applicant);
            }

            OnCountChanged(Total.Length);
        }

        protected virtual void OnCountChanged(int value) {
            if (CountChanged != null) {
                CountChanged(this, new IntEventArgs() { value = value });
            }
        }
    }
}