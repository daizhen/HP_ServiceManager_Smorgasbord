using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace SM.Smorgasbord.GKAutomatic.Connections
{
    [Serializable]
    public class PackageChains
    {
        private Collection<ChainInfo> chains = new Collection<ChainInfo>();

        public Collection<ChainInfo> Chains
        {
            get
            {
                return chains;
            }
        }

        public ChainInfo this[string chainName]
        {
            get
            {
                foreach (ChainInfo chain in chains)
                {
                    if (chain.Name.Equals(chainName))
                    {
                        return chain;
                    }
                }
                return null;
            }
        }
    }
}
