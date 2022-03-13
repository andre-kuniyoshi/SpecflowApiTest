
@AdicionaListaEventos
Feature: Listar eventos da agenda
	Como Usuário/Administrador 
	Quero listar um ou mais eventos da agenda usando filtros
	Para obter uma lista filtrada dos eventos da Vaivoa

Background:
	Given que estou autenticado no sistema
	And a rota do endpoint é 'eventos' e o método http é 'Get'

Scenario Outline: Listar todos os eventos utilizando filtro de datas
	Given que quero buscar os eventos entre '<dataInicio>' e '<dataFim>'
	When faco a requisição
	Then retorna uma resposta com o status igual a 'Ok'
	And com o campo sucesso do body da resposta igual a 'true'
	And retorna '<numEventos>' eventos
	Examples:
		| dataInicio  | dataFim    | numEventos |
		| 2022-10-01  | 2022-10-15 |     15     |
		| 2022-10-15  | 2022-10-31 |     8      |
		| 2022-10-16  | 2022-10-19 |     0      |

Scenario: Listar todos eventos filtrado por tipo de evento que existe
	Given que quero buscar um evento com um tipo que 'existe' na agenda
	When faco a requisição
	Then retorna uma resposta com o status igual a 'Ok'
	And com o campo sucesso do body da resposta igual a 'false'
	And retorna '0' eventos

Scenario: Tentar listar todos eventos filtrado por tipo de evento que não existe
	Given que quero buscar um evento com um tipo 'inexistente' na agenda
	When faco a requisição
	Then retorna uma resposta com o status igual a 'BadRequest'
	And com o campo sucesso do body da resposta igual a 'false'
	And retorna '0' eventos
#Scenario: Listar todos eventos filtrando por apresentador do evento
#Scenario: Tentar listar todos eventos filtrando por um apresentador que não existe
