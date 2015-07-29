#pragma once
#include<m_stdafx.h>

using std::string;
using std::vector;
using std::set;
using std::shared_ptr;
using std::ostream;

class QueryResult
{
	friend ostream& operator <<(ostream &, const QueryResult &);
public:
	QueryResult(const string &s,shared_ptr<set<size_t>> pset,shared_ptr<vector<string>> plines_vec);

	set<size_t>::iterator begin() const;
	set<size_t>::iterator end() const;
	shared_ptr<vector<string>> get_pFile() const;

private:
	string m_target;
	shared_ptr<set<size_t>> m_plno_set;
	shared_ptr<vector<string>> m_pFile;

};

inline ostream& operator <<(ostream &os, const QueryResult &qr)
{
	os << qr.m_target << " occurs " << qr.m_plno_set->size() << " "
		<< "times" << "\n";
	auto beg = qr.m_pFile->begin();
	for (auto &index : *qr.m_plno_set)
	{
		os << "\t(line " << index + 1 << ") ";
		os << *(beg+index) << "\n";
	}
	return os;
}