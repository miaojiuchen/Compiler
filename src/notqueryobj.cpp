#include<notqueryobj.h>

using std::make_shared;

QueryResult NotQueryObj::eval(const TextQuerier &tq) const
{
	auto qset = m_qexp.eval(tq);
	auto ret_set = make_shared<set<size_t>>();
	auto beg = qset.begin(), end = qset.end();

	auto size = qset.get_pFile()->size();
	for (size_t i = 0; i != size; ++i)
	{
		if (beg == end || *beg != i)
		{
			ret_set->insert(i);
		}
		else if (beg != end)
		{
			beg++;
		}
	}
	return QueryResult(get_exp(), ret_set, qset.get_pFile());
}

string NotQueryObj::get_exp() const
{
	return "!(" + m_qexp.get_exp() + ")";
}