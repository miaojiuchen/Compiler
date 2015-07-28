#include<andqueryobj.h>
#include<m_stdafx.h>

using std::set_intersection;
using std::make_shared;
using std::inserter;

QueryResult AndQueryObj::eval(const TextQuerier &tq) const
{
	QueryResult lset = m_lexp.eval(tq), rset = m_rexp.eval(tq);
	auto ret_set = make_shared<set<size_t>>();
	set_intersection(
		lset.begin(), lset.end(),//ctnr1
		rset.begin(), rset.end(),//ctnr2
		inserter(*ret_set, ret_set->begin())//destination
		);
	return QueryResult(this->get_exp(), ret_set, lset.get_pFile());
}