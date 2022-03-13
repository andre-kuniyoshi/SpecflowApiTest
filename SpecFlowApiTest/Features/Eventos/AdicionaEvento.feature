#language: pt-br

Funcionalidade: Adiciona evento
	Como Usuário/Administrador 
	Quero agendar um evento
	Para que entre na agenda de eventos da Vaivoa

Contexto:
	Dado que estou autenticado no sistema
	E a rota do endpoint é 'eventos' e o método http é 'POST'

Cenario: Agendar um evento com dados validos
	Dado que quero agendar um evento valido
	Quando faco a requisição
	Entao retorna uma resposta com o status igual a 'Created'
	E com o campo sucesso do body da resposta igual a 'true'

Esquema do Cenário: Agendar um evento em um horario ja ocupado
	Dado que ja existe um evento valido agendado no dia '<dia>'
	E que quero agendar um evento valido no '<dia>' das '<horaInicio>' até '<horaFim>'
	Quando faco a requisição
	Entao retorna uma resposta com o status igual a 'BadRequest'
	E com o campo sucesso do body da resposta igual a 'false'
	Exemplos:
		| dia  | horaInicio | horaFim |
		| 01   | 13:00      | 13:30   |
		| 02   | 13:30      | 14:00   |
		| 03   | 13:00      | 14:00   |

Cenario: Agendar um evento fora do horario de inicio permitido
	Dado que quero agendar um evento valido fora do horario permitido
	Quando faco a requisição
	Entao retorna uma resposta com o status igual a 'BadRequest'
	E com o campo sucesso do body da resposta igual a 'false'

Cenario: Agendar um evento o qual o tipo de evento não esta cadastrado no sistema
	Dado que quero agendar um evento valido com um tipo inexistente no sistema
	Quando faco a requisição
	Entao retorna uma resposta com o status igual a 'BadRequest'
	E com o campo sucesso do body da resposta igual a 'false'

Cenario: Agendar um evento faltando dados
	Dado que quero agendar um evento invalido
	Quando faco a requisição
	Entao retorna uma resposta com o status igual a 'BadRequest'
	E com o campo sucesso do body da resposta igual a 'false'
