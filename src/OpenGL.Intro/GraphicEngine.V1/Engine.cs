﻿using GraphicEngine.V1.Entities;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;

namespace GraphicEngine.V1
{
    public class Engine
    {
        /// <summary>
        /// [VBO&VAO] Создает фигуру с шагом 3
        /// </summary>
        /// <param name="vertices">Vertices of Element</param>
        /// <returns>Vertex Array Object of Element</returns>
        public int Create(float[] vertices)
        {
            var vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            var vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            return vao;
        }

        /// <summary>
        /// [VBO&VAO&EBO] Создает фигуру с шагом 3
        /// </summary>
        /// <param name="indices">Порядок отрисовки</param>
        /// <returns>Vertices Array Object of Element</returns>
        public int Create(float[] vertices, uint[] indices)
        {
            var vao = CreateVerticesArrayObject(vertices);
            CreateElementsArrayBuffer(indices);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            return vao;
        }

        public int CreateWithoutBinding(float[] vertices, uint[] indices)
        {
            var vao = CreateVerticesArrayObject(vertices);
            CreateElementsArrayBuffer(indices);

            return vao;
        }

        /// <summary>
        /// [VBO&VAO] Создает фигуру с шагом 3
        /// </summary>
        /// <param name="indices">Порядок отрисовки</param>
        /// <returns>Vertices Array Object of Element</returns>
        public int CreateTextured(float[] vertices)
        {
            var vao = CreateVerticesArrayObject(vertices);

            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            return vao;
        }

        /// <summary>
        /// [VBO&VAO&EBO] Создает фигуру с шагом 3
        /// </summary>
        /// <param name="indices">Порядок отрисовки</param>
        /// <returns>Vertices Array Object of Element</returns>
        public int CreateTextured(float[] vertices, uint[] indices)
        {
            var vao = CreateVerticesArrayObject(vertices);
            CreateElementsArrayBuffer(indices);

            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            return vao;
        }

        public int CreateColoredTextured(float[] vertices, uint[] indices)
        {
            var vao = CreateVerticesArrayObject(vertices);
            CreateElementsArrayBuffer(indices);

            GL.EnableVertexAttribArray(0); // position
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);

            GL.EnableVertexAttribArray(1); // color
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));

            GL.EnableVertexAttribArray(2); // texture position
            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));

            return vao;
        }

        public int CreateTransformation(float[] vertices, uint[] indices, Shader shader)
        {
            var vao = CreateVerticesArrayObject(vertices);
            CreateElementsArrayBuffer(indices);

            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

            var rotate = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(45.0f)); // rotate and scale
            //var scale = Matrix4.CreateScale(0.5f);
            //var transformation = matrix * scale;
            var transformation = rotate;

            shader.Use();
            var location = GL.GetUniformLocation(shader.Id, "transform");
            if (location == -1) throw new InvalidOperationException("Не удалось обнаружить позицию uniform в шейдере");
            GL.UniformMatrix4(location, true, ref transformation);

            return vao;
        }

        private int CreateVerticesArrayObject(float[] vertices)
        {
            var vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            var vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            return vao;
        }

        private int CreateElementsArrayBuffer(uint[] indices)
        {
            var ebo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(float), indices, BufferUsageHint.StaticDraw);

            return ebo;
        }
    }
}
