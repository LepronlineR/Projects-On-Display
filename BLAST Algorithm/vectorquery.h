#ifndef _vectorquery_
#define _vectorquery_

#include <list>
#include <string>
#include <vector>

class VectorQuery{
    public:
        // Typedef
        // < (k-mer, genome sequence location), (k-mer, genome sequence location)... >
        typedef std::vector<std::pair<std::string, std::list<unsigned int> > > vector_type;
        // Constructor
        VectorQuery(const std::string& g);
        // Printer ( This is a test function )
        void print();
        // Modifiers
        void createVector(int k_mer);
        // Query
        void findQuery(const std::string& kmer_query, const std::string& query, int mismatch);
    private:
        std::string genome;
        vector_type vector;

        // Helper Methods
        int compareMismatches(const std::string& str, const std::string& compared, int mismatch);
        void insert(const std::string& key, unsigned int index, vector_type& v);
};

#endif