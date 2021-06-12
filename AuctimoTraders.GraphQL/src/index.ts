import 'reflect-metadata'
import * as tq from 'type-graphql'
import { ApolloServer } from 'apollo-server'
import { PrismaClient } from '@prisma/client'
import {
  CreateAppRolesResolver,
  FindUniqueAppRolesResolver,
  FindManyAppRolesResolver,
  CreateCountriesResolver,
  FindUniqueCountriesResolver,
  FindManyCountriesResolver,
  CreateItemTypesResolver,
  FindUniqueItemTypesResolver,
  FindManyItemTypesResolver,
  CreateRegionsResolver,
  FindUniqueRegionsResolver,
  FindManyRegionsResolver,
  CreateSalesResolver,
  FindUniqueSalesResolver,
  FindManySalesResolver,
  CreateUserRolesResolver,
  FindUniqueUserRolesResolver,
  FindManyUserRolesResolver,
  AppRolesRelationsResolver,
  CountriesRelationsResolver,
  ItemTypesRelationsResolver,
  RegionsRelationsResolver,
  SalesRelationsResolver,
  UserRolesRelationsResolver,
  AppUsersRelationsResolver,
  CreateAppUsersResolver,
  FindUniqueAppUsersResolver,
  FindManyAppUsersResolver
} from "@generated/type-graphql";

const port = process.env.PORT || 4000

const prisma = new PrismaClient()

const app = async () => {
  const schema = await tq.buildSchema({
    resolvers: [
      CreateAppRolesResolver,
      FindUniqueAppRolesResolver,
      FindManyAppRolesResolver,
      CreateCountriesResolver,
      FindUniqueCountriesResolver,
      FindManyCountriesResolver,
      CreateItemTypesResolver,
      FindUniqueItemTypesResolver,
      FindManyItemTypesResolver,
      CreateRegionsResolver,
      FindUniqueRegionsResolver,
      FindManyRegionsResolver,
      CreateSalesResolver,
      FindUniqueSalesResolver,
      FindManySalesResolver,
      CreateUserRolesResolver,
      FindUniqueUserRolesResolver,
      FindManyUserRolesResolver,
      AppRolesRelationsResolver,
      CountriesRelationsResolver,
      ItemTypesRelationsResolver,
      RegionsRelationsResolver,
      SalesRelationsResolver,
      UserRolesRelationsResolver,
      AppUsersRelationsResolver,
      CreateAppUsersResolver,
      FindUniqueAppUsersResolver,
      FindManyAppUsersResolver
    ],
    emitSchemaFile: true
  })

  const context = () => {
    return {
      prisma
    }
  }
  new ApolloServer({ schema, context }).listen({ port }, () =>
    console.log(`🚀 Server ready at: http://localhost:${port}`)
  )
}

app()