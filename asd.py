

import networkx as nx
import matplotlib.pyplot as plt

G1=nx.DiGraph()
G1.add_node(0)
G1.add_node(1)
G1.add_node(2)
G1.add_node(3)
G1.add_node(4)

G1.add_edge(0, 1, weight = 5)
G1.add_edge(0, 2, weight = 3)
G1.add_edge(0, 4, weight = 2)
G1.add_edge(1, 2, weight = 2.0)
G1.add_edge(1, 3, weight = 6)
G1.add_edge(2, 1, weight = 1)
G1.add_edge(2, 3, weight = 2.0)
G1.add_edge(4, 1, weight = 6)
G1.add_edge(4, 2, weight = 10.0)
G1.add_edge(4, 3, weight = 4)

node_pos=nx.get_node_attributes(G1,'pos')
node_col = ['white' if not node in sp else 'red' for node in G1.nodes()]
edge_col = ['black' if not edge in red_edges else 'red' for edge in G1.edges()]
nx.draw_networkx(G1, 1,node_color= node_col, node_size=450)
nx.draw_networkx_edges(G1, node_pos,edge_color= edge_col)
nx.draw_networkx_edge_labels(G1, node_pos,edge_color= edge_col, edge_labels=arc_weight)
plt.axis('off')
plt.show()
















G1.clear()

