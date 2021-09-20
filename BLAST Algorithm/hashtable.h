#ifndef _hashtable_
#define _hashtable_

#include <list>
#include <string>
#include <vector>

class HashTable{
    public:
        // Typedef
        // < (k-mer, genome sequence location), (k-mer, genome sequence location)... >
        typedef std::vector<std::pair<std::string, std::list<unsigned int> > > table_type;
        // Constructor
        HashTable(float o, int ts, const std::string& g);
        // Printer ( This is a test function )
        void print();
        // Modifiers
        void createTable(int k_mer);
        // Query
        void findQuery(const std::string& kmer_query, const std::string& query, int mismatch);
    private:
        std::string genome;
        table_type table;
        float occupancy;
        int occupied;

        // Hash Function
        unsigned int hashFunction(const std::string& str);

        // Helper Methods
        int compareMismatches(const std::string& str, const std::string& compared, int mismatch);
        std::list<unsigned int> getIndexes(const std::string& str, bool& obtained);
        float calculateOccupancy() const { return (float) occupied/table.size(); }
        void increaseOccupancy() { occupied++; }
        void resize();
        void insert(const std::string& key, unsigned int index, unsigned int pos, table_type& tb);
        bool valid(unsigned int pos) const { return pos < table.size(); } 
};

#endif