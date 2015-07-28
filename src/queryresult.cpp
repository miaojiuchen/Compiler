#include<queryresult.h>

QueryResult::QueryResult(
	const string &s,
	shared_ptr<set<size_t>> pset,
	shared_ptr<vector<string>> plines_vec) 
	:m_target(s), m_plno_set(pset), m_pFile(plines_vec)
{
}

set<size_t>::iterator QueryResult::begin() const
{
	return m_plno_set->begin();
}

set<size_t>::iterator QueryResult::end() const
{
	return m_plno_set->end();
}

shared_ptr<vector<string>> QueryResult::get_pFile() const
{
	return m_pFile;
}