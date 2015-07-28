#include<textquerier.h>

using std::istringstream;
using std::make_shared;

TextQuerier::TextQuerier(ifstream &file) :m_plines_vec(new vector<string>)
{
	//≥ı ºªØlines_vec
	//m_plines_vec = make_shared<vector<string>>();
	string l_content;
	size_t l_index = 0;
	while (getline(file, l_content))
	{
		m_plines_vec->push_back(l_content);
		string word;
		istringstream iss(l_content);
		while (iss >> word)
		{
			auto &word_set = m_word_map[word];
			if (!word_set)
			{
				word_set.reset(new set<size_t>);
			}
			word_set->insert(l_index);
		}
		++l_index;
	}
}

QueryResult TextQuerier::query(const string target) const
{
	static shared_ptr<set<size_t>> empty_data(new set<size_t>);
	auto found = m_word_map.find(target);
	if (found == m_word_map.end())
	{
		return QueryResult(target, empty_data, m_plines_vec);
	}
	else
	{
		return QueryResult(target, found->second, m_plines_vec);
	}
}