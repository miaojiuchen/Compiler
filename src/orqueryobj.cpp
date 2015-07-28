#include<orqueryobj.h>

using std::make_shared;

QueryResult OrQueryObj::eval(const TextQuerier &tq) const
{
	auto lqres = m_lexp.eval(tq), rqres = m_rexp.eval(tq);
	auto pret_set = make_shared<set<size_t>>(lqres.begin(), lqres.end());
	pret_set->insert(rqres.begin(), rqres.end());
	return QueryResult(get_exp(), pret_set, lqres.get_pFile());
}